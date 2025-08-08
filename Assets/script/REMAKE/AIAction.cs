using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIAction
{
    
    public static GameObject FindNearestArmy(GameObject enemy)
    {
        GameObject[] listUnitPlayer = GameObject.FindGameObjectsWithTag("Player");
        Vector3 enemyPos = enemy.transform.position;
        float minDistance = float.MaxValue;
        GameObject nearestTargetUnit = null;
        foreach (GameObject unitPlayer in listUnitPlayer)
        {
            float distance = Vector3.Distance(enemyPos, unitPlayer.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTargetUnit = unitPlayer;
            }
        }
        return nearestTargetUnit;
    }
    public static GameObject FindNearestCity(GameObject enemy)
    {
        GameObject[] listTargetCity = GameObject.FindGameObjectsWithTag("City");
        Vector3 enemyPos = enemy.transform.position;
        float minDistance = float.MaxValue;
        GameObject nearestTarget = null;
        foreach (GameObject targetCity in listTargetCity)
        {
            if (targetCity.GetComponent<ClassCity>().isPlayerCity)
            {
                float distance = Vector3.Distance(enemyPos, targetCity.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = targetCity;
                }
            }
        }
        return nearestTarget;
    }
    public static bool isNearestTargetIsCity(GameObject enemy)
    {
        GameObject[] listPlayerUnit = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] listCities = GameObject.FindGameObjectsWithTag("City");
        Vector3 enemyPos = enemy.transform.position;
        float nearestPlayerDist = float.MaxValue;
        foreach (var player in listPlayerUnit)
            nearestPlayerDist = Mathf.Min(nearestPlayerDist, Vector3.Distance(enemyPos, player.transform.position));
        float nearestCityDist = float.MaxValue;
        foreach (var city in listCities)
            if(city.GetComponent<ClassCity>().isPlayerCity)
                nearestCityDist = Mathf.Min(nearestCityDist, Vector3.Distance(enemyPos, city.transform.position));
        return nearestCityDist <= nearestPlayerDist;
    }

    public static bool IsTargetInRange(GameObject enemy, GameObject target)
    {
        ClassUnit enemyUnit = enemy.GetComponent<ClassUnit>();
        Vector3 enemyPos = enemy.transform.position;
        Vector3 targetPos = target.transform.position;

        float range = enemyUnit.RangeAtk * 100f;
        float distance = Mathf.Abs(enemyPos.x - targetPos.x) + Mathf.Abs(enemyPos.y - targetPos.y);

        return distance < range;
    }

    public static void EnemyMove(GameObject enemy)
    {
        bool isTargetIsCity;
        ClassUnit enemyUnit = enemy.GetComponent<ClassUnit>();
        GameObject nearestTarget;
        if (isNearestTargetIsCity(enemy))
        {
            nearestTarget = FindNearestCity(enemy);
            isTargetIsCity = true;
        }
        else
        {
            nearestTarget = FindNearestArmy(enemy);
            isTargetIsCity = false;
        }
        if (nearestTarget == null)
            return;
        Vector3 moveToPos;
        if (IsTargetInRange(enemy, nearestTarget))
        {
            if (isTargetIsCity && nearestTarget.GetComponent<ClassCity>().cityHp>0)
            {
                nearestTarget.GetComponent<ClassCity>().TakeDamage(enemyUnit.TotalDame(),nearestTarget);
                enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
                return;
            }
            else if (!isTargetIsCity)
            {
                enemyUnit.Attack(nearestTarget);
                enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
                return;
            }
        }

        moveToPos = FindBestMovePosition(enemy, nearestTarget);

        // Nếu tìm được vị trí để di chuyển
        if (moveToPos != enemy.transform.position)
        {
            enemyUnit.Move(moveToPos);
        }

        // Sau khi di chuyển, nếu đã vào tầm thì tấn công
        if (IsTargetInRange(enemy, nearestTarget))
        {
            enemyUnit.Attack(nearestTarget);
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