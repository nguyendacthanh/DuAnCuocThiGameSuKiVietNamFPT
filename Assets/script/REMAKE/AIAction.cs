using System.Collections.Generic;
using UnityEngine;

public class AIAction
{
    public static GameObject FindNearestArmy(GameObject enemy)
    {
        GameObject[] listUnitPlayer = GameObject.FindGameObjectsWithTag("Player");
        Vector3 viTriEnemy = enemy.transform.position;
        float minDistance = float.MaxValue;
        GameObject nearestTargetUnit = null;
        foreach (GameObject unitPlayer in listUnitPlayer)
        {
            float distance = Vector3.Distance(viTriEnemy, unitPlayer.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTargetUnit = unitPlayer;
            }
        }
        return nearestTargetUnit;
    }

    public static bool IsPlayerInRange(GameObject enemy, GameObject player)
    {
        ClassUnit enemyUnit = enemy.GetComponent<ClassUnit>();
        Vector3 enemyPos = enemy.transform.position;
        Vector3 playerPos = player.transform.position;

        float range = enemyUnit.RangeAtk * 100f;
        float distance = Mathf.Abs(enemyPos.x - playerPos.x) + Mathf.Abs(enemyPos.y - playerPos.y);

        return distance <= range;
    }

    public static void EnemyMove(GameObject enemy)
    {
        ClassUnit enemyUnit = enemy.GetComponent<ClassUnit>();
        GameObject nearestPlayer = FindNearestArmy(enemy);

        if (nearestPlayer == null)
            return;

        if (IsPlayerInRange(enemy, nearestPlayer))
        {
            enemyUnit.Attack(nearestPlayer);
            enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
            return;
        }

        Vector3 moveToPos = FindBestMovePosition(enemy, nearestPlayer);

        // Nếu tìm được vị trí để di chuyển
        if (moveToPos != enemy.transform.position)
        {
            enemyUnit.Move(moveToPos);
        }

        // Sau khi di chuyển, nếu đã vào tầm thì tấn công
        if (IsPlayerInRange(enemy, nearestPlayer))
        {
            enemyUnit.Attack(nearestPlayer);
            enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
        }
    }

    private static Vector3 FindBestMovePosition(GameObject enemy, GameObject target)
    {
        ClassUnit enemyUnit = enemy.GetComponent<ClassUnit>();
        Vector3 currentPos = enemy.transform.position;
        int speed = enemyUnit.Speed;

        HashSet<Vector3> cantMovePositions = new HashSet<Vector3>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            cantMovePositions.Add(SnapToGrid(go.transform.position));
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (go != enemy)
                cantMovePositions.Add(SnapToGrid(go.transform.position));
        }

        List<Vector3> possibleMoves = new List<Vector3>();

        for (int dx = -speed; dx <= speed; dx++)
        {
            for (int dy = -speed; dy <= speed; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= speed)
                {
                    Vector3 newPos = currentPos + new Vector3(dx * 100f, dy * 100f, 0);
                    Vector3 snapped = SnapToGrid(newPos);
                    if (!cantMovePositions.Contains(snapped))
                    {
                        possibleMoves.Add(snapped);
                    }
                }
            }
        }

        if (possibleMoves.Count == 0)
            return currentPos;

        // Tìm vị trí gần player nhất
        Vector3 bestPos = currentPos;
        float minDistance = float.MaxValue;
        Vector3 playerPos = target.transform.position;

        foreach (var pos in possibleMoves)
        {
            float dist = Vector3.Distance(pos, playerPos);
            if (dist < minDistance)
            {
                minDistance = dist;
                bestPos = pos;
            }
        }

        return bestPos;
    }

    private static Vector3 SnapToGrid(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x / 100f) * 100f,
            Mathf.Round(pos.y / 100f) * 100f,
            0
        );
    }
}