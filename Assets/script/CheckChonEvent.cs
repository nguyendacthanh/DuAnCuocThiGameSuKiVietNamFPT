using System.Collections.Generic;
using UnityEngine;

public class CheckChonEvent
{
    public List<GameObject> armies { get; private set; } = new List<GameObject>();
    public List<GameObject> enemies { get; private set; } = new List<GameObject>();

    public void CapNhatToaDo()
    {
        armies.Clear();
        enemies.Clear();

        armies.AddRange(GameObject.FindGameObjectsWithTag("Army"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    // Hàm kiểm tra có đơn vị tại vị trí không, trả về GameObject nếu có
    private GameObject TimTheoViTri(Vector2 clickPos, List<GameObject> list)
    {
        foreach (var obj in list)
        {
            if ((Vector2)obj.transform.position == clickPos)
            {
                return obj;
            }
        }
        return null;
    }

    // Trả về GameObject Enemy nếu trúng, ngược lại trả về null
    public GameObject TimEnemy(Vector2 clickPos)
    {
        return TimTheoViTri(clickPos, enemies);
    }

    // Trả về GameObject Army nếu trúng, ngược lại trả về null
    public GameObject TimArmy(Vector2 clickPos)
    {
        return TimTheoViTri(clickPos, armies);
    }
    
    public List<GameObject> TimEnemyXungQuanh(GameObject Army)
    {
        List<GameObject> ketQua = new List<GameObject>();

        if (Army == null) return ketQua;

        Vector2 viTri = Army.transform.position;
        Vector2[] viTriCanKiemTra = new Vector2[]
        {
            viTri + new Vector2(100, 0), // phải
            viTri + new Vector2(-100, 0), // trái
            viTri + new Vector2(0, 100), // trên
            viTri + new Vector2(0, -100), // dưới
        };

        foreach (var viTriXungQuanh in viTriCanKiemTra)
        {
            GameObject enemy = TimEnemy(viTriXungQuanh);
            if (enemy != null)
            {
                ketQua.Add(enemy);
            }
        }

        return ketQua;
    }
}