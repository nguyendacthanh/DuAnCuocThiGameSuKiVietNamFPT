using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class actions
{
    //lay toa do click
    public static Vector2 ToaDoClick(Vector2 clickPos)
    {
        float x = Mathf.Round(clickPos.x / 100f) * 100f;
        float y = Mathf.Round(clickPos.y / 100f) * 100f;
        return new Vector2(x, y);
        
    }
    
    //hien thi grid
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
    
    //tam di chuyen cua player
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
    
    //
    private List<GameObject> Player = new List<GameObject>();
    private List<GameObject> Enemy = new List<GameObject>();

    // cập nhật lại danh sách của army và enemy mỗi khi có hành động xảy ra
    public void CapNhatToaDo()
    {
        Player.Clear();
        Enemy.Clear();
        Player.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        Enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private GameObject TimTheoViTri(Vector2 ClickPossition, List<GameObject> list)
    {
        foreach (var obj in list)
        {
            if ((Vector2)obj.transform.position == ClickPossition)
            {
                return obj; 
            }
        }
        return null;
    }

    public GameObject TimEnemy(Vector2 clickPosition)
    {
        return TimTheoViTri(clickPosition, Enemy);
    }

    public GameObject TimPlayer(Vector2 clickPosition)
    {
        return TimTheoViTri(clickPosition, Player);
    }
    public List<GameObject> TimEnemyTrongTamDanh(GameObject player, int RangeAtk)
    {
        List<GameObject> ketQua = new List<GameObject>();

        if (player == null) return ketQua;

        Vector2 viTriGoc = player.transform.position;

        for (int dx = -RangeAtk; dx <= RangeAtk; dx++)
        {
            for (int dy = -RangeAtk; dy <= RangeAtk; dy++)
            {
                // Hình thoi: chỉ lấy các điểm có khoảng cách Manhattan <= tocDo
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= RangeAtk)
                {
                    Vector2 viTriCanKiemTra = viTriGoc + new Vector2(dx * 100, dy * 100);

                    GameObject enemy = TimEnemy(viTriCanKiemTra);
                    if (enemy != null && !ketQua.Contains(enemy))
                    {
                        ketQua.Add(enemy);
                    }
                }
            }
        }

        return ketQua;
    }

}
