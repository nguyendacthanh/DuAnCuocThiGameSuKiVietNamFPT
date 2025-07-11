using System.Collections.Generic;
using UnityEngine;

public class QuanLySuKienChonDonVi : MonoBehaviour
{
    public float KhoangCachGioiHanXY = 50f;
    public float KhoangCachClickToiGrid = 30f;

    public GameObject GridDiChuyen, GridChon;

    private GameObject DonViDangChon = null;
    private bool gridActive = false;

    private ChonDonVi unitSelector;
    private Grid gridHandler;

    void Start()
    {
        unitSelector = new ChonDonVi(KhoangCachGioiHanXY);
        gridHandler = new Grid(GridChon, GridDiChuyen);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(mousePos.x, mousePos.y);

            GameObject DonViDuocChon = unitSelector.TimDonViGanNhat(clickPos);

            // 👉 Ưu tiên xử lý nếu bấm vào một ô GridDiChuyen
            if (DonViDangChon != null && gridActive)
            {
                List<Vector3> viTriGrid = gridHandler.LayDanhSachViTriGridDiChuyen();
                foreach (Vector3 viTri in viTriGrid)
                {
                    if (Vector2.Distance(clickPos, viTri) <= KhoangCachClickToiGrid)
                    {
                        classDonVi scriptDonVi = DonViDangChon.GetComponent<classDonVi>();
                        scriptDonVi.DiChuyenDen(viTri);

                        if (scriptDonVi.LuotDiChuyen <= 0)
                        {
                            gridHandler.XoaTatCaGrid();
                            gridActive = false;
                            Debug.Log("Hết lượt di chuyển.");
                        }
                        else
                        {
                            int tocDo = Mathf.RoundToInt(scriptDonVi.TocDo);
                            gridHandler.XoaTatCaGrid();
                            gridHandler.TaoOGridHinhThoi(DonViDangChon.transform.position, tocDo);
                        }

                        return; // ✅ Đã xử lý click rồi → thoát
                    }
                }
            }

            // 👉 Xử lý khi bấm chọn đơn vị
            if (DonViDuocChon == null)
            {
                Debug.Log("Không có đơn vị nào trong phạm vi.");
                gridHandler.XoaTatCaGrid();
                gridActive = false;
                DonViDangChon = null;
                return;
            }

            if (DonViDangChon == null || DonViDuocChon != DonViDangChon)
            {
                DonViDangChon = DonViDuocChon;
                gridActive = true;
                Debug.Log("Đã chọn " + DonViDuocChon.name);

                int tocDo = Mathf.RoundToInt(DonViDuocChon.GetComponent<classDonVi>().TocDo);
                gridHandler.XoaTatCaGrid();
                gridHandler.TaoOGridHinhThoi(DonViDuocChon.transform.position, tocDo);
            }
            else
            {
                // Toggle grid nếu click lại đơn vị đang chọn
                if (gridActive)
                {
                    gridHandler.XoaTatCaGrid();
                    gridActive = false;
                    Debug.Log("Tắt grid");
                }
                else
                {
                    gridActive = true;
                    int tocDo = Mathf.RoundToInt(DonViDuocChon.GetComponent<classDonVi>().TocDo);
                    gridHandler.TaoOGridHinhThoi(DonViDuocChon.transform.position, tocDo);
                    Debug.Log("Bật grid");
                }
            }
        }
    }
}
