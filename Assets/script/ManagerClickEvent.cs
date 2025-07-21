using System.Collections.Generic;
using UnityEngine;

public class ManagerClickEvent : MonoBehaviour
{
    // Prefab các loại grid
    public GameObject prefabGridEnemy, prefabGridChon, prefabGridDiChuyen, prefabGridAttack;

    private CheckChonEvent checker;
    private Grid grid;

    // Lưu vị trí các grid hiện tại
    private Vector2? viTriGridEnemy = null, viTriGridChon = null, viTriGridDiChuyen = null;

    void Start()
    {
        checker = new CheckChonEvent();
        grid = new Grid();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Lấy tọa độ click trong thế giới
            Vector2 clickWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 toaDoClick = click.ToaDoClick(clickWorld); // Tọa độ lưới

            checker.CapNhatToaDo(); // Cập nhật danh sách đơn vị

            GameObject enemyObj = checker.TimEnemy(toaDoClick);
            GameObject armyObj = checker.TimArmy(toaDoClick);

            // ======== CLICK ENEMY ============
            if (enemyObj != null)
            {
                if (viTriGridEnemy != null && viTriGridEnemy == toaDoClick)
                {
                    // Nếu click lại cùng vị trí => tắt grid enemy
                    XoaGridTheoTag("GridEnemy");
                    viTriGridEnemy = null;
                }
                else
                {
                    // Hiển thị grid enemy mới
                    XoaGridTheoTag("GridEnemy");
                    grid.HienThiGridEnemy(toaDoClick, prefabGridEnemy);
                    viTriGridEnemy = toaDoClick;

                    // Tắt các grid khác
                    XoaGridTheoTag("Grid");
                    XoaGridTheoTag("GridMove");
                    XoaGridTheoTag("GridAttack");
                    viTriGridChon = null;
                    viTriGridDiChuyen = null;
                }

                // Bỏ chọn tất cả army
                foreach (GameObject army in checker.armies)
                    army.GetComponent<classDonVi>().isSelected = false;
            }

            // ======== CLICK ARMY ============
            else if (armyObj != null)
            {
                classDonVi donVi = armyObj.GetComponent<classDonVi>();

                // Nếu đơn vị đang được chọn => bỏ chọn
                if (donVi.isSelected)
                {
                    donVi.isSelected = false;
                    XoaGridTheoTag("GridMove");
                    XoaGridTheoTag("Grid");
                    XoaGridTheoTag("GridAttack");
                    viTriGridDiChuyen = null;
                }
                else
                {
                    // Bỏ chọn tất cả army khác
                    foreach (GameObject army in checker.armies)
                        army.GetComponent<classDonVi>().isSelected = false;

                    donVi.isSelected = true;
                    XoaGridTheoTag("GridAttack");
                    if (donVi.LuotTanCong > 0)
                    {
                        List<GameObject> enemiesXungQuanh = checker.TimEnemyXungQuanh(donVi.gameObject);
                        foreach (GameObject enemy in enemiesXungQuanh)
                        {
                            grid.HienThiGridAttack(enemy.transform.position, prefabGridAttack);
                        }
                    }
                    // Xóa grid di chuyển cũ
                    XoaGridTheoTag("GridMove");
                    XoaGridTheoTag("Grid");
                    
                    grid.HienThiGridChon(toaDoClick, prefabGridChon);
                    if (donVi.LuotDiChuyen > 0)
                    {
                        // Tính danh sách tọa độ di chuyển hình thoi
                        List<Vector2> danhSachToaDo = ToaDoHinhThoi.TinhToaDo(donVi.transform.position, donVi.TocDo);

                        foreach (Vector2 pos in danhSachToaDo)
                        {
                            // Chỉ hiển thị grid nếu vị trí đó không có army hoặc enemy
                            if (checker.TimArmy(pos) == null && checker.TimEnemy(pos) == null)
                            {
                                grid.HienThiGridDiChuyen(pos, prefabGridDiChuyen);
                            }
                        }

                        viTriGridDiChuyen = donVi.transform.position;
                    }

                    // Xóa các grid không cần thiết
                    XoaGridTheoTag("Grid");
                    XoaGridTheoTag("GridEnemy");
                    
                    grid.HienThiGridChon(toaDoClick, prefabGridChon);
                    viTriGridChon = null;
                    viTriGridEnemy = null;
                }
            }

            // ======== CLICK Ô TRỐNG ============
            else
            {
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
                }

                // Xóa các grid khác
                XoaGridTheoTag("GridEnemy");
                XoaGridTheoTag("GridMove");
                XoaGridTheoTag("GridAttack");
                viTriGridEnemy = null;
                viTriGridDiChuyen = null;

                // Bỏ chọn tất cả army
                foreach (GameObject army in checker.armies)
                    army.GetComponent<classDonVi>().isSelected = false;
            }
        }
    }

    // Hàm xóa toàn bộ grid theo tag
    void XoaGridTheoTag(string tag)
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject g in grids)
        {
            Destroy(g);
        }
    }
}
