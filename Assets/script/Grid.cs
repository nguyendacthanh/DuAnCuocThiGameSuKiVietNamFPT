using UnityEngine;

public class Grid
{
    public void HienThiGridDiChuyen(Vector2 viTri, GameObject prefab)
    {
        Object.Instantiate(prefab, viTri, Quaternion.identity);
    }

    public void HienThiGridChon(Vector2 viTri, GameObject prefab)
    {
        Object.Instantiate(prefab, viTri, Quaternion.identity);
    }

    public void HienThiGridAttack(Vector2 viTri, GameObject prefab)
    {
        Object.Instantiate(prefab, viTri, Quaternion.identity);
    }

    public void HienThiGridEnemy(Vector2 viTri, GameObject prefab)
    {
        Object.Instantiate(prefab, viTri, Quaternion.identity);
    }
}
