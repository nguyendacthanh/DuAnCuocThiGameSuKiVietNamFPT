using UnityEngine;

public class QuanLySuKienChonDonVi : MonoBehaviour
{
        public float KhoangCachGioiHanXY = 50f;
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

            if (DonViDuocChon == null)
            {
                Debug.Log("Không có đơn vị nào trong phạm vi.");
                gridHandler.XoaTatCaGrid();
                gridActive = false;
                DonViDangChon = null;
                return;
            }

            // Nếu chọn đơn vị mới
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
                // Toggle grid nếu bấm lại cùng đơn vị
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
