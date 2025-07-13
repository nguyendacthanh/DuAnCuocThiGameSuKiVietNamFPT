using UnityEngine;
using System.Collections.Generic;
public class XuLyClick
{
    private ChonDonVi unitSelector;
    private Grid gridHandler;
    private GameObject gridChon, gridDiChuyen, gridAttack;
    private float khoangCachGioiHan, khoangCachClickToiGrid, banKinhTanCong;

    private GameObject donViDangChon = null;
    private bool gridActive = false;

    public XuLyClick(ChonDonVi selector, Grid grid, GameObject gridChon, GameObject gridDiChuyen, GameObject gridAttack,
                     float khoangCachGioiHan, float khoangCachClickToiGrid, float banKinhTanCong)
    {
        this.unitSelector = selector;
        this.gridHandler = grid;
        this.gridChon = gridChon;
        this.gridDiChuyen = gridDiChuyen;
        this.gridAttack = gridAttack;
        this.khoangCachGioiHan = khoangCachGioiHan;
        this.khoangCachClickToiGrid = khoangCachClickToiGrid;
        this.banKinhTanCong = banKinhTanCong;
    }

    public void XuLyClickNguoiChoi()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clickPos = new Vector2(mousePos.x, mousePos.y);

        GameObject donViDuocChon = unitSelector.TimDonViGanNhat(clickPos);

        // Đang chọn và grid đang hiển thị
        if (donViDangChon != null && gridActive)
        {
            List<Vector3> viTriGrid = gridHandler.LayDanhSachViTriGridDiChuyen();
            foreach (Vector3 viTri in viTriGrid)
            {
                if (Vector2.Distance(clickPos, viTri) <= khoangCachClickToiGrid)
                {
                    classDonVi scriptDonVi = donViDangChon.GetComponent<classDonVi>();
                    scriptDonVi.DiChuyenDen(viTri);

                    if (scriptDonVi.LuotDiChuyen <= 0)
                    {
                        gridHandler.XoaTatCaGrid();
                        gridActive = false;

                        if (scriptDonVi.LuotTanCong > 0)
                        {
                            List<Vector3> viTriKeDich = LayCacViTriKeDichGan(donViDangChon.transform.position);
                            foreach (Vector3 viTriEnemy in viTriKeDich)
                            {
                                Object.Instantiate(gridAttack, viTriEnemy, Quaternion.identity);
                            }
                        }
                    }
                    else
                    {
                        gridHandler.XoaTatCaGrid();

                        List<Vector3> viTriKeDich = scriptDonVi.LuotTanCong > 0
                            ? LayCacViTriKeDichGan(donViDangChon.transform.position)
                            : new List<Vector3>();

                        int tocDo = Mathf.RoundToInt(scriptDonVi.TocDo);
                        gridHandler.TaoOGridHinhThoi(donViDangChon.transform.position, tocDo, viTriKeDich);
                    }

                    return;
                }
            }
        }

        if (donViDuocChon == null)
        {
            Debug.Log("Không có đơn vị nào trong phạm vi.");
            gridHandler.XoaTatCaGrid();
            gridActive = false;
            donViDangChon = null;
            return;
        }

        // Chọn đơn vị mới
        if (donViDangChon == null || donViDuocChon != donViDangChon)
        {
            donViDangChon = donViDuocChon;
            gridActive = true;
            gridHandler.XoaTatCaGrid();
            Debug.Log("Đã chọn " + donViDuocChon.name);

            classDonVi script = donViDangChon.GetComponent<classDonVi>();

            if (script.LuotDiChuyen <= 0)
            {
                Object.Instantiate(gridChon, donViDangChon.transform.position, Quaternion.identity);

                if (script.LuotTanCong > 0)
                {
                    List<Vector3> viTriKeDich = LayCacViTriKeDichGan(donViDangChon.transform.position);
                    foreach (Vector3 viTriEnemy in viTriKeDich)
                    {
                        Object.Instantiate(gridAttack, viTriEnemy, Quaternion.identity);
                    }
                }
            }
            else
            {
                List<Vector3> viTriKeDich = script.LuotTanCong > 0
                    ? LayCacViTriKeDichGan(donViDangChon.transform.position)
                    : new List<Vector3>();

                int tocDo = Mathf.RoundToInt(script.TocDo);
                gridHandler.TaoOGridHinhThoi(donViDangChon.transform.position, tocDo, viTriKeDich);
            }
        }
        else
        {
            // Toggle bật / tắt grid
            if (gridActive)
            {
                gridHandler.XoaTatCaGrid();
                gridActive = false;
                Debug.Log("Tắt grid");
            }
            else
            {
                gridActive = true;
                classDonVi script = donViDuocChon.GetComponent<classDonVi>();

                if (script.LuotDiChuyen <= 0)
                {
                    Object.Instantiate(gridChon, donViDuocChon.transform.position, Quaternion.identity);

                    if (script.LuotTanCong > 0)
                    {
                        List<Vector3> viTriKeDich = LayCacViTriKeDichGan(donViDuocChon.transform.position);
                        foreach (Vector3 viTriEnemy in viTriKeDich)
                        {
                            Object.Instantiate(gridAttack, viTriEnemy, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    List<Vector3> viTriKeDich = script.LuotTanCong > 0
                        ? LayCacViTriKeDichGan(donViDuocChon.transform.position)
                        : new List<Vector3>();

                    int tocDo = Mathf.RoundToInt(script.TocDo);
                    gridHandler.TaoOGridHinhThoi(donViDuocChon.transform.position, tocDo, viTriKeDich);
                }

                Debug.Log("Bật grid");
            }
        }
    }

    private List<Vector3> LayCacViTriKeDichGan(Vector3 viTriDonVi)
    {
        List<Vector3> viTriKeDich = new List<Vector3>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(viTriDonVi, enemy.transform.position);
            if (dist <= banKinhTanCong)
            {
                viTriKeDich.Add(enemy.transform.position);
            }
        }

        return viTriKeDich;
    }
}
