using UnityEngine;

public class ManagerClickEvent : MonoBehaviour
{
    public GameObject prefabGridEnemy;
    public GameObject prefabGridChon;

    private CheckChonEvent checker;
    private Grid grid;

    private Vector2? viTriGridEnemy = null;
    private Vector2? viTriGridChon = null;

    void Start()
    {
        checker = new CheckChonEvent();
        grid = new Grid();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 toaDoClick = click.ToaDoClick(clickWorld);

            checker.CapNhatToaDo();

            bool isEnemy = checker.TimToaDoEnemy(toaDoClick);
            bool isArmy = checker.TimToaDoArmy(toaDoClick);

            if (isEnemy)
            {
                // Toggle GridEnemy
                if (viTriGridEnemy != null && viTriGridEnemy == toaDoClick)
                {
                    XoaGridTheoTag("GridEnemy");
                    viTriGridEnemy = null;
                }
                else
                {
                    XoaGridTheoTag("GridEnemy");
                    grid.HienThiGridEnemy(toaDoClick, prefabGridEnemy);
                    viTriGridEnemy = toaDoClick;

                    // Xóa GridChon nếu có
                    XoaGridTheoTag("Grid");
                    viTriGridChon = null;
                }
            }
            else
            {
                // Toggle GridChon
                if (viTriGridChon != null && viTriGridChon == toaDoClick)
                {
                    XoaGridTheoTag("Grid");
                    viTriGridChon = null;
                }
                else
                {
                    XoaGridTheoTag("Grid");
                    grid.HienThiGridChon(toaDoClick, prefabGridChon);
                    viTriGridChon = toaDoClick;

                    // Xóa GridEnemy nếu có
                    XoaGridTheoTag("GridEnemy");
                    viTriGridEnemy = null;

                    if (isArmy)
                    {
                        Debug.Log("Click vào Army: chuẩn bị xử lý thêm (VD: spawn GridDiChuyen nếu chon == true)");
                        // TODO: xử lý chọn đơn vị, kiểm tra LuotDiChuyen, gọi hàm hiển thị GridDiChuyen nếu cần
                    }
                }
            }
        }
    }

    void XoaGridTheoTag(string tag)
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject g in grids)
        {
            GameObject.Destroy(g);
        }
    }
}
