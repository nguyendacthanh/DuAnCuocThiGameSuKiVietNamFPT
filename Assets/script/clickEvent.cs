using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
        public float KhoangCachGioiHanXY = 50f; // Giới hạn khoảng cách khi click
    private GameObject DonViDangChon = null;
    
    // Các prefab cho grid: một grid đặc biệt cho vị trí đơn vị (GridChon)
    // và các grid di chuyển xung quanh (GridDiChuyen)
    public GameObject GridDiChuyen, GridChon;
    
    // Biến theo dõi trạng thái grid: true nếu grid đang hiển thị, false nếu không
    private bool gridActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Lấy vị trí click trong thế giới
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(mousePos.x, mousePos.y);

            // Tìm đơn vị nằm trong giới hạn click
            GameObject DonViDuocChon = GioiHanKhoangCachChonDonVi(clickPos);

            // Nếu không click vào đơn vị nào (click ra ngoài phạm vi)
            if (DonViDuocChon == null)
            {
                Debug.Log("Không có đơn vị nào trong phạm vi 50 đơn vị.");
                // Xóa hết tất cả grid và reset trạng thái
                XoaTatCaGrid();
                gridActive = false;
                DonViDangChon = null;
                return;
            }

            // Nếu chưa có đơn vị nào được chọn hoặc đơn vị mới khác với đơn vị đang được chọn
            if (DonViDangChon == null || DonViDangChon != DonViDuocChon)
            {
                // Mỗi khi chọn đơn vị mới, xóa grid cũ
                XoaTatCaGrid();
                // Cập nhật đơn vị mới
                DonViDangChon = DonViDuocChon;
                gridActive = true; // đánh dấu là grid đang được hiển thị
                Debug.Log("Đã chọn " + DonViDuocChon.name);

                // Lấy thông số và tạo grid theo hình thoi
                classDonVi ThongSo = DonViDuocChon.GetComponent<classDonVi>();
                int tocDo = Mathf.RoundToInt(ThongSo.TocDo);
                TaoOGridHinhThoi(DonViDuocChon.transform.position, tocDo);
            }
            else
            {
                // Click lại vào cùng một đơn vị đã chọn → toggle grid
                if (gridActive)
                {
                    // Grid đang hiển thị → xóa đi
                    XoaTatCaGrid();
                    gridActive = false;
                    Debug.Log("Tắt grid cho " + DonViDuocChon.name);
                }
                else
                {
                    // Grid đang ẩn → tạo lại grid
                    gridActive = true;
                    Debug.Log("Bật grid cho " + DonViDuocChon.name);
                    classDonVi ThongSo = DonViDuocChon.GetComponent<classDonVi>();
                    int tocDo = Mathf.RoundToInt(ThongSo.TocDo);
                    TaoOGridHinhThoi(DonViDuocChon.transform.position, tocDo);
                }
            }
        }
    }

    // Hàm tìm đơn vị gần click trong phạm vi giới hạn (50 đơn vị)
    GameObject GioiHanKhoangCachChonDonVi(Vector2 clickPos)
    {
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
        float minDist = Mathf.Infinity;
        GameObject NearestArmy = null;

        foreach (GameObject army in armies)
        {
            Vector2 armyPos = army.transform.position;
            float dx = Mathf.Abs(armyPos.x - clickPos.x);
            float dy = Mathf.Abs(armyPos.y - clickPos.y);

            if (dx <= KhoangCachGioiHanXY && dy <= KhoangCachGioiHanXY)
            {
                float dist = Vector2.Distance(clickPos, armyPos);
                if (dist < minDist)
                {
                    minDist = dist;
                    NearestArmy = army;
                }
            }
        }
        return NearestArmy;
    }

    // Hàm xóa tất cả các grid đang tồn tại (các đối tượng có tag "Grid")
    void XoaTatCaGrid()
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("Grid");
        foreach (GameObject grid in grids)
        {
            Destroy(grid);
        }
    }

    // Hàm tạo grid theo hình thoi với vị trí gốc (tâm) của đơn vị được chọn
    // và bán kính (số ô dựa trên tốc độ, TocDo)
    void TaoOGridHinhThoi(Vector3 goc, int banKinh)
    {
        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                // Điều kiện tạo grid theo khoảng cách Manhattan (hình thoi)
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector3 viTriMoi = goc + new Vector3(dx * 100, dy * 100, 0);
                    
                    // Nếu là ô trung tâm thì dùng prefab GridChon, các ô còn lại dùng prefab GridDiChuyen
                    if (dx == 0 && dy == 0)
                        Instantiate(GridChon, viTriMoi, Quaternion.identity);
                    else
                        Instantiate(GridDiChuyen, viTriMoi, Quaternion.identity);
                }
            }
        }
    }
}
