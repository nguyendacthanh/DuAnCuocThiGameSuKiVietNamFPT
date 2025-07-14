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
            if (go != null) Object.Destroy(go);
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

    public void HienThiGridAttack(Vector3 viTriGoc, int tamTanCong)
    {
        float banKinh = tamTanCong * 100f;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector3 viTriEnemy = enemy.transform.position;
            float dist = Vector3.Distance(viTriEnemy, viTriGoc);
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
        List<Vector3> viTriBiChan = LayTatCaViTriDonVi();

        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector3 viTriMoi = goc + new Vector3(dx * 100, dy * 100, 0);

                    // ✅ Dùng đúng biến viTriBiChan
                    if (viTriBiChan.Contains(viTriMoi))
                        continue;

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

private List<Vector3> LayTatCaViTriDonVi()
{
    List<Vector3> viTris = new List<Vector3>();
    GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Army");
    GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

    foreach (GameObject unit in allUnits)
        viTris.Add(unit.transform.position);

    foreach (GameObject enemy in allEnemies)
        viTris.Add(enemy.transform.position);

    return viTris;
    }

    public List<Vector3> LayDanhSachViTriGridDiChuyen() => viTriManager.LayTatCa();
    public int SoLuongOGridDiChuyen() => viTriManager.DemSoLuong();
}
