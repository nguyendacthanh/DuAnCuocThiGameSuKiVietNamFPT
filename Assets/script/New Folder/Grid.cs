using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private List<GameObject> dsGrid = new List<GameObject>();

    private GameObject gridChon;
    private GameObject gridDiChuyen;

    private ViTriGridDiChuyen viTriManager = new ViTriGridDiChuyen();

    public Grid(GameObject gridChon, GameObject gridDiChuyen)
    {
        this.gridChon = gridChon;
        this.gridDiChuyen = gridDiChuyen;
    }

    public void TaoOGridHinhThoi(Vector3 goc, int banKinh)
    {
        XoaTatCaGrid();
        

        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector3 viTriMoi = goc + new Vector3(dx * 100, dy * 100, 0);

                    if (dx == 0 && dy == 0)
                    {
                        GameObject grid = Object.Instantiate(gridChon, viTriMoi, Quaternion.identity);
                        dsGrid.Add(grid);
                    }
                    else
                    {
                        GameObject grid = Object.Instantiate(gridDiChuyen, viTriMoi, Quaternion.identity);
                        dsGrid.Add(grid);
                        viTriManager.ThemViTri(viTriMoi);
                    }
                }
            }
        }
    }

    public void XoaTatCaGrid()
    {
        foreach (GameObject grid in dsGrid)
        {
            if (grid != null)
                Object.Destroy(grid);
        }

        dsGrid.Clear();
        viTriManager.XoaTatCa();
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
