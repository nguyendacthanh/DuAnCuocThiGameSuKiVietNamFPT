using System.Collections.Generic;
using UnityEngine;

public class actions
{
    public static Vector2 ToaDoClick(Vector2 clickPos)
    {
        float x = Mathf.Round(clickPos.x / 100f) * 100f;
        float y = Mathf.Round(clickPos.y / 100f) * 100f;
        return new Vector2(x, y);
        
    }
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
    public static List<Vector2> TamDiChuyen(Vector2 GameObjectPosition, int banKinh)
    {
        List<Vector2> ketQua = new List<Vector2>();

        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector2 diem = GameObjectPosition + new Vector2(dx * 100, dy * 100);
                    ketQua.Add(diem);
                }
            }
        }

        return ketQua;
    }
}
