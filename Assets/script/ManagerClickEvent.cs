using System.Collections.Generic;
using UnityEngine;

public class ManagerClickEvent : MonoBehaviour
{
    public GameObject prefabGridEnemy;
    public GameObject prefabGridChon;
    public GameObject prefabGridDiChuyen;

    private CheckChonEvent checker;
    private Grid grid;

    private Vector2? viTriGridEnemy = null;
    private Vector2? viTriGridChon = null;

    void Start()
    {
        checker = new CheckChonEvent();
        grid = new Grid();

        if (prefabGridChon == null || prefabGridDiChuyen == null || prefabGridEnemy == null)
        {
            Debug.LogError("❌ Một hoặc nhiều prefab chưa được gán trong Inspector!");
        }
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
            else if (isArmy)
            {
                foreach (GameObject go in checker.armies)
                {
                    if ((Vector2)go.transform.position == toaDoClick)
                    {
                        classDonVi donVi = go.GetComponent<classDonVi>();
                        if (!donVi.chon)
                        {
                            donVi.chon = true;
                            Debug.Log("✅ Army được chọn: " + donVi.name);

                            // Spawn GridChon
                            XoaGridTheoTag("Grid");
                            grid.HienThiGridChon(toaDoClick, prefabGridChon);
                            viTriGridChon = toaDoClick;

                            // Spawn GridDiChuyen nếu còn lượt
                            if (donVi.LuotDiChuyen > 0)
                            {
                                List<Vector2> diemDiChuyen = ToaDoHinhThoi.TinhToaDo(toaDoClick, Mathf.RoundToInt(donVi.TocDo));
                                foreach (Vector2 diem in diemDiChuyen)
                                {
                                    bool trungDonVi = checker.ToaDoEnemy.Contains(diem) || checker.ToaDoArmy.Contains(diem);
                                    if (!trungDonVi)
                                    {
                                        grid.HienThiGridDiChuyen(diem, prefabGridDiChuyen);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Bỏ chọn
                            donVi.chon = false;
                            Debug.Log("❌ Bỏ chọn Army: " + donVi.name);
                            XoaGridTheoTag("Grid");
                            XoaGridTheoTag("GridMove");
                            viTriGridChon = null;
                        }
                        break;
                    }
                }

                // Luôn xóa GridEnemy nếu click vào army
                XoaGridTheoTag("GridEnemy");
                viTriGridEnemy = null;
            }
            else
            {
                // Toggle GridChon theo vị trí click
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
