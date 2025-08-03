using System.Collections.Generic;
using UnityEngine;

public class AIAction
{
    public static GameObject FindNearestArmy(GameObject enemy)
    {
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Player");
        Vector3 viTriEnemy = enemy.transform.position;
        float minDistance = float.MaxValue;
        GameObject nearestArmy = null;

        foreach (GameObject army in armies)
        {
            float distance = Vector3.Distance(viTriEnemy, army.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestArmy = army;
            }
        }

        return nearestArmy;
    }
    //Kiểm tra có enemy trong tầm đánh của các gameobject enemy không, nếu có trả về true và ko di chuyển, thay vào đó tấn công, nếu 
    //không thì enemy sẽ di chuyển
    public static bool isArmyPlayerInRange(GameObject armyEnemy, GameObject armyPlayer)
    {
        ClassDonVi enemyDonVi = armyEnemy.GetComponent<ClassDonVi>();// biến enemyDonVi sẽ chứa toàn bộ đặc điểm của enemy
                                                                     // truyền vào và sẽ lấy tầm đánh làm bán kính để check
        Vector3 armyPlayerPos = armyPlayer.transform.position;
        Vector3 armyEnemyPos = armyEnemy.transform.position;
        if (Mathf.Abs(armyPlayerPos.x - armyEnemyPos.x) + Mathf.Abs(armyPlayerPos.y - armyEnemyPos.y) <= enemyDonVi.RangeAtk * 100f)
        {
            return true;
        }
        return false;
    }
    public static Vector3 MovePosition(GameObject armyEnemy, GameObject armyPlayer)
    {
        ClassDonVi armyE = armyEnemy.GetComponent<ClassDonVi>();
        ClassDonVi armyP = armyPlayer.GetComponent<ClassDonVi>();
        Vector3 armyPlayerPos = armyPlayer.transform.position;
        Vector3 armyEnemyPos = armyE.transform.position;
        int tocDo = armyE.Speed;

        // Nếu mục tiêu đã nằm trong tầm tấn công thì không di chuyển
        if (isArmyPlayerInRange(armyEnemy, armyPlayer))
        {
            armyE.CurrentSpeed = Mathf.Max(0, armyE.CurrentSpeed - 1);
            armyE.Attack(armyPlayer,armyEnemy);
        }
        // Lấy tất cả vị trí các Army và Enemy hiện có
        HashSet<Vector3> cantMovePosition = new HashSet<Vector3>();
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject a in armies)
        {
            Vector3 pos = new Vector3(
                Mathf.Round(a.transform.position.x / 100f) * 100f,
                Mathf.Round(a.transform.position.y / 100f) * 100f,
                0
            );
            cantMovePosition.Add(pos);
        }

        foreach (GameObject e in enemies)
        {
            if (e != armyE)
            {
                Vector3 pos = new Vector3(
                    Mathf.Round(e.transform.position.x / 100f) * 100f,
                    Mathf.Round(e.transform.position.y / 100f) * 100f,
                    0
                );
                cantMovePosition.Add(pos);
            }
        }

        // Tìm các vị trí trống trong tầm di chuyển
        List<Vector3> viTriCoTheDiChuyen = new List<Vector3>();
        for (int dx = -tocDo; dx <= tocDo; dx++)
        {
            for (int dy = -tocDo; dy <= tocDo; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= tocDo && (dx == 0 || dy == 0))
                {
                    Vector3 pos = armyEnemyPos + new Vector3(dx * 100f, dy * 100f, 0);
                    Vector3 posSnap = new Vector3(
                        Mathf.Round(pos.x / 100f) * 100f,
                        Mathf.Round(pos.y / 100f) * 100f,
                        Mathf.Round(pos.z / 100f) * 100f
                    );

                    if (!cantMovePosition.Contains(posSnap))
                    {
                        viTriCoTheDiChuyen.Add(posSnap);
                    }
                }
            }
        }

        // Không có vị trí hợp lệ → đứng yên và trừ lượt
        if (viTriCoTheDiChuyen.Count == 0)
        {
            armyE.CurrentSpeed = Mathf.Max(0, armyE.CurrentSpeed - 1);
            return armyEnemyPos;
        }

        // Chọn vị trí gần mục tiêu nhất
        Vector3 viTriTotNhat = viTriCoTheDiChuyen[0];
        float minDistance = Vector3.Distance(viTriTotNhat, armyPlayerPos);
        foreach (Vector3 pos in viTriCoTheDiChuyen)
        {
            float dist = Vector3.Distance(pos, armyPlayerPos);
            if (dist < minDistance)
            {
                minDistance = dist;
                viTriTotNhat = pos;
            }
        }

        return viTriTotNhat;
    }
    public static void EnemyMove(GameObject enemy)
    {
        GameObject armyNearest = FindNearestArmy(enemy);
        Vector3 viTriMoi = MovePosition(enemy, armyNearest);
        ClassDonVi enemyDonVi = enemy.GetComponent<ClassDonVi>();
        enemyDonVi.Move(viTriMoi);
    }

    public static List<Vector3> TamDiChuyen(Vector3 gameObjectPosition, int banKinh)
    {
        List<Vector3> ketQua = new List<Vector3>();

        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector3 diem = gameObjectPosition + new Vector3(dx * 100f, dy * 100f, 0);
                    ketQua.Add(diem);
                }
            }
        }

        return ketQua;
    }
}
