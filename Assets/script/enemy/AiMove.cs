using UnityEngine;
using System.Collections;

public class AiMove
{
    // Trả về vị trí của Army gần nhất
    public static Vector3 FindNearestArmy(GameObject enemy)
    {
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
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
        classDonVi enemyDonVi = enemy.GetComponent<classDonVi>();
        Vector3 startPos = enemy.transform.position;
        int tocDo = enemyDonVi.TocDo;

        // Tính hướng di chuyển mỗi trục
        int dx = (int)Mathf.Sign(mucTieu.x - startPos.x);
        int dy = (int)Mathf.Sign(mucTieu.y - startPos.y);

        // Xem có thể đi bao nhiêu bước
        int distanceX = Mathf.Abs(Mathf.RoundToInt((mucTieu.x - startPos.x) / 100f));
        int distanceY = Mathf.Abs(Mathf.RoundToInt((mucTieu.y - startPos.y) / 100f));

        int buocX = Mathf.Min(distanceX, tocDo);
        int buocY = Mathf.Min(distanceY, tocDo - buocX);

        Vector3 newPos = startPos + new Vector3(buocX * dx * 100f, buocY * dy * 100f, 0);
        return newPos;
    }

    // Gọi tới classDonVi.DiChuyenDen để enemy di chuyển tới vị trí đã tính
    public static void EnemyMove(GameObject enemy, Vector3 mucTieu)
    {
        Vector3 viTriMoi = MovePosition(enemy, mucTieu);
        classDonVi enemyDonVi = enemy.GetComponent<classDonVi>();
        enemyDonVi.DiChuyenDen(viTriMoi);
    }
}
