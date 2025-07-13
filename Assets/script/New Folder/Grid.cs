using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private List<GameObject> dsGrid = new List<GameObject>();
    private GameObject gridChon;
    private GameObject gridDiChuyen;
    private GameObject gridAttack;

    private ViTriGridDiChuyen viTriManager = new ViTriGridDiChuyen();

    public Grid(GameObject gridChon, GameObject gridDiChuyen, GameObject gridAttack)
    {
        this.gridChon = gridChon;
        this.gridDiChuyen = gridDiChuyen;
        this.gridAttack = gridAttack;
    }

    public void XoaTatCaGrid()
    {
        foreach (GameObject go in dsGrid)
        {
            if (go != null)
                Object.Destroy(go);
        }

        dsGrid.Clear();
        viTriManager.XoaTatCa();
    }

    public void HienThiGridChon(Vector3 viTri)
    {
        GameObject go = Object.Instantiate(gridChon, viTri, Quaternion.identity);
        go.layer = 6;
        dsGrid.Add(go); 
    }

    public void HienThiGridAttack(Vector3 viTriGoc, float banKinh)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector3 viTriEnemy = enemy.transform.position;
            float dist = Vector3.Distance(viTriEnemy, viTriGoc);

            // Kiểm tra nếu trong khoảng 150 đơn vị
            if (dist <= banKinh)
            {
                GameObject atk = Object.Instantiate(gridAttack, viTriEnemy, Quaternion.identity);
                atk.layer = 6;
                dsGrid.Add(atk);
            }
        }
    }

    public void TaoOGridHinhThoi(Vector3 goc, int banKinh)
    {
        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector3 viTriMoi = goc + new Vector3(dx * 100, dy * 100, 0);

                    // Vị trí chính giữa
                    if (dx == 0 && dy == 0)
                    {
                        HienThiGridChon(viTriMoi);
                        continue;
                    }

                    GameObject oMoi = Object.Instantiate(gridDiChuyen, viTriMoi, Quaternion.identity);
                    oMoi.layer = 5;
                    dsGrid.Add(oMoi);
                    viTriManager.ThemViTri(viTriMoi);
                }
            }
        }
    }

    public List<Vector3> LayDanhSachViTriGridDiChuyen()
    {
        return viTriManager.LayTatCa();
    }

    public int SoLuongOGridDiChuyen()
    {
        return viTriManager.DemSoLuong();
    }
}
