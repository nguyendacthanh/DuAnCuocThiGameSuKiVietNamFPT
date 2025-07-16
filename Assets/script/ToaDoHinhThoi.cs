using System.Collections.Generic;
using UnityEngine;

public class ToaDoHinhThoi
{
    public static List<Vector2> TinhToaDo(Vector2 tam, int banKinh)
    {
        List<Vector2> ketQua = new List<Vector2>();

        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector2 diem = tam + new Vector2(dx * 100, dy * 100);
                    ketQua.Add(diem);
                }
            }
        }

        return ketQua;
    }
}
