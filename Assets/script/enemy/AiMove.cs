using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiMove
{
    // Trả về vị trí của Army gần nhất
    public static Vector3 FindNearestArmy(GameObject enemy)
    {
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Player");
        Vector3 viTriEnemy = enemy.transform.position;
        float minDistance = float.MaxValue;
        Vector3 nearestArmyPos = viTriEnemy;

        foreach (GameObject army in armies)
        {
            float distance = Vector3.Distance(viTriEnemy, army.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestArmyPos = army.transform.position;
            }
        }

        return nearestArmyPos;
    }

    // Tìm vị trí hợp lệ gần hơn mục tiêu trong phạm vi tốc độ
    public static Vector3 MovePosition(GameObject enemy, Vector3 mucTieu)
    {
        ClassDonVi enemyDonVi = enemy.GetComponent<ClassDonVi>();
        Vector3 startPos = enemy.transform.position;
        int tocDo = enemyDonVi.Speed;

        // Nếu mục tiêu đã nằm trong tầm tấn công thì không di chuyển
        

        // Lấy tất cả vị trí các Army và Enemy hiện có
        HashSet<Vector3> viTriBiChan = new HashSet<Vector3>();
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject a in armies)
        {
            Vector3 pos = new Vector3(
                Mathf.Round(a.transform.position.x / 100f) * 100f,
                Mathf.Round(a.transform.position.y / 100f) * 100f,
                0
            );
            viTriBiChan.Add(pos);
        }

        foreach (GameObject e in enemies)
        {
            if (e != enemy) // tránh tự cản chính mình
            {
                Vector3 pos = new Vector3(
                    Mathf.Round(e.transform.position.x / 100f) * 100f,
                    Mathf.Round(e.transform.position.y / 100f) * 100f,
                    0
                );
                viTriBiChan.Add(pos);
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
                    Vector3 pos = startPos + new Vector3(dx * 100f, dy * 100f, 0);
                    Vector3 posSnap = new Vector3(
                        Mathf.Round(pos.x / 100f) * 100f,
                        Mathf.Round(pos.y / 100f) * 100f,
                        0
                    );

                    if (!viTriBiChan.Contains(posSnap))
                    {
                        viTriCoTheDiChuyen.Add(posSnap);
                    }
                }
            }
        }
        float khoangCach = Vector3.Distance(startPos, mucTieu);
        if (khoangCach <= enemyDonVi.RangeAtk * 100f)
        {
            enemyDonVi.CurrentSpeed = Mathf.Max(0, enemyDonVi.CurrentSpeed - 1);
            return startPos;
        }

        // Không có vị trí hợp lệ → đứng yên và trừ lượt
        if (viTriCoTheDiChuyen.Count == 0)
        {
            enemyDonVi.CurrentSpeed = Mathf.Max(0, enemyDonVi.CurrentSpeed - 1);
            return startPos;
        }

        // Chọn vị trí gần mục tiêu nhất
        Vector3 viTriTotNhat = viTriCoTheDiChuyen[0];
        float minDistance = Vector3.Distance(viTriTotNhat, mucTieu);
        foreach (Vector3 pos in viTriCoTheDiChuyen)
        {
            float dist = Vector3.Distance(pos, mucTieu);
            if (dist < minDistance)
            {
                minDistance = dist;
                viTriTotNhat = pos;
            }
        }

        return viTriTotNhat;
    }

    // Gọi tới classDonVi.DiChuyenDen để enemy di chuyển tới vị trí đã tính
    public static void EnemyMove(GameObject enemy, Vector3 mucTieu)
    {
        Vector3 viTriMoi = MovePosition(enemy, mucTieu);
        ClassDonVi enemyDonVi = enemy.GetComponent<ClassDonVi>();
        enemyDonVi.Move(viTriMoi);
    }
}