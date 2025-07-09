using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public float KhoangCachGioiHanXY = 50f; // khoảng cách giới hạn khi click
    // chuột thì đơn vị được chọn, nếu không có đơn vị nào trong phạm vi xy=50 của
    // cú click chuột thì sẽ không có sự kiện chọn đơn vị diễn ra.
    private GameObject DonViDangChon = null;
    public GameObject gridPrefab;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(mousePos.x, mousePos.y);

            GameObject DonViDuocChon = GioiHanKhoangCachChonDonVi(clickPos);

            if (DonViDuocChon != null)
            {
                DonViDangChon = DonViDuocChon;
                Debug.Log("Đã chọn " + DonViDuocChon.name);
                classDonVi ThongSo = DonViDuocChon.GetComponent<classDonVi>();
                int tocDo = Mathf.RoundToInt(ThongSo.TocDo);
                TaoOGridHinhThoi(DonViDuocChon.transform.position, tocDo);
            }
            else
            {
                Debug.Log("Không có Don Vi nào trong phạm vi 50 đơn vị.");
            }
        }
    }

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
    void TaoOGridHinhThoi(Vector3 goc, int banKinh)
    {
        // Vị trí gốc là vị trí đơn vị được chọn (ô xanh)
        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector3 viTriMoi = goc + new Vector3(dx*100, dy*100, 0);
                    Instantiate(gridPrefab, viTriMoi, Quaternion.identity);
                }
            }
        }
    }
}
