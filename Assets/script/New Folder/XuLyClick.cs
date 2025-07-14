using UnityEngine;
using System.Collections.Generic;
public class XuLyClick
{
        private ChonDonVi unitSelector;
    private Grid gridHandler;
    private float khoangCachGioiHan, khoangCachClickToiGrid;

    private GameObject donViDangChon = null;
    private bool gridActive = false;

    public XuLyClick(ChonDonVi selector, Grid grid, float khoangCachGioiHan, float khoangCachClickToiGrid)
    {
        this.unitSelector = selector;
        this.gridHandler = grid;
        this.khoangCachGioiHan = khoangCachGioiHan;
        this.khoangCachClickToiGrid = khoangCachClickToiGrid;
    }

    public void XuLyClickNguoiChoi()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clickPos = new Vector2(mousePos.x, mousePos.y);

        GameObject donViDuocChon = unitSelector.TimDonViGanNhat(clickPos);

        // ✅ Tấn công nếu click vào grid attack
        if (donViDangChon != null && gridActive)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(clickPos, khoangCachClickToiGrid);
            foreach (var hit in hits)
            {
                if (hit.gameObject.CompareTag("GridAttack"))
                {
                    classDonVi scriptDonVi = donViDangChon.GetComponent<classDonVi>();
                    if (scriptDonVi.LuotTanCong > 0)
                    {
                        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                        foreach (GameObject enemy in enemies)
                        {
                            if (enemy.transform.position == hit.transform.position)
                            {
                                classDonVi enemyScript = enemy.GetComponent<classDonVi>();
                                scriptDonVi.TanCong(enemyScript);
                                scriptDonVi.LuotTanCong--;

                                // Sau khi tấn công, kiểm tra có cần giữ gridAttack không
                                CapNhatGridAttack(scriptDonVi);
                                return;
                            }
                        }
                    }
                }
            }
        }

        // ✅ Di chuyển nếu click vào grid di chuyển
        if (donViDangChon != null && gridActive)
        {
            List<Vector3> viTriGrid = gridHandler.LayDanhSachViTriGridDiChuyen();
            foreach (Vector3 viTri in viTriGrid)
            {
                if (Vector2.Distance(clickPos, viTri) <= khoangCachClickToiGrid)
                {
                    classDonVi scriptDonVi = donViDangChon.GetComponent<classDonVi>();
                    scriptDonVi.DiChuyenDen(viTri);

                    gridHandler.XoaTatCaGrid();
                    gridActive = false;

                    // ✅ Kiểm tra có cần hiển thị lại gridAttack không
                    CapNhatGridAttack(scriptDonVi);
                    return;
                }
            }
        }

        // ✅ Nếu bấm vào chỗ trống
        if (donViDuocChon == null)
        {
            gridHandler.XoaTatCaGrid();
            gridActive = false;
            donViDangChon = null;
            return;
        }

        // ✅ Chọn đơn vị mới
        if (donViDangChon == null || donViDuocChon != donViDangChon)
        {
            donViDangChon = donViDuocChon;
            gridActive = true;

            gridHandler.XoaTatCaGrid();
            classDonVi script = donViDangChon.GetComponent<classDonVi>();

            if (script.LuotDiChuyen <= 0)
            {
                gridHandler.HienThiGridChon(donViDangChon.transform.position);
            }
            else
            {
                gridHandler.TaoOGridHinhThoi(donViDangChon.transform.position, Mathf.RoundToInt(script.TocDo));
            }

            // ✅ Hiện GridAttack nếu có lượt tấn công
            CapNhatGridAttack(script);
        }
        else
        {
            // ✅ Bấm lại đơn vị để bật/tắt grid
            if (gridActive)
            {
                gridHandler.XoaTatCaGrid();
                gridActive = false;
            }
            else
            {
                gridActive = true;
                classDonVi script = donViDuocChon.GetComponent<classDonVi>();

                if (script.LuotDiChuyen <= 0)
                {
                    gridHandler.HienThiGridChon(donViDuocChon.transform.position);
                }
                else
                {
                    gridHandler.TaoOGridHinhThoi(donViDuocChon.transform.position, Mathf.RoundToInt(script.TocDo));
                }

                CapNhatGridAttack(script);
            }
        }
    }

    // ✅ Hàm cập nhật grid attack
    private void CapNhatGridAttack(classDonVi script)
    {
        if (script.LuotTanCong <= 0)
        {
            gridHandler.XoaTatCaGrid();
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool enemyTrongTam = false;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, script.transform.position);
            if (dist <= script.tamTanCong * 100)
            {
                enemyTrongTam = true;
                break;
            }
        }

        if (enemyTrongTam)
        {
            gridHandler.HienThiGridAttack(script.transform.position, script.tamTanCong);
        }
        else
        {
            gridHandler.XoaTatCaGrid();
        }
    }
}
