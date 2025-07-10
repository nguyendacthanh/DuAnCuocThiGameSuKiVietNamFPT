using UnityEngine;

public class Grid
{
    private GameObject gridChon;
    private GameObject gridDiChuyen;

    public Grid(GameObject gridChon, GameObject gridDiChuyen)
    {
        this.gridChon = gridChon;
        this.gridDiChuyen = gridDiChuyen;
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

                    if (dx == 0 && dy == 0)
                        Object.Instantiate(gridChon, viTriMoi, Quaternion.identity);
                    else
                        Object.Instantiate(gridDiChuyen, viTriMoi, Quaternion.identity);
                }
            }
        }
    }

    public void XoaTatCaGrid()
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("Grid");
        foreach (GameObject grid in grids)
        {
            Object.Destroy(grid);
        }
    }
}
