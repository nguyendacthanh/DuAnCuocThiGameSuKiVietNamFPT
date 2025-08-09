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
        return nearestCityDist < nearestPlayerDist;
    }

    public static bool IsTargetInRange(GameObject enemy, GameObject target)
    {
        ClassUnit enemyUnit = enemy.GetComponent<ClassUnit>();
        Vector3 enemyPos = enemy.transform.position;
        Vector3 targetPos = target.transform.position;

        float range = enemyUnit.RangeAtk * 100f;
        float distance = Mathf.Abs(enemyPos.x - targetPos.x) + Mathf.Abs(enemyPos.y - targetPos.y);

        return distance <= range;
    }

    public static void EnemyMove(GameObject enemy)
    {
        bool isTargetIsCity = true;
        ClassUnit enemyUnit = enemy.GetComponent<ClassUnit>();
        GameObject targetUnit = FindNearestArmy(enemy);
        GameObject targetCity = FindNearestCity(enemy);
        ClassCity cityAdj = null;
        if (targetCity != null)
            cityAdj = targetCity.GetComponent<ClassCity>();

        ClassUnit unitAdj = null;
        if (targetUnit != null)
            unitAdj = targetUnit.GetComponent<ClassUnit>();
        bool isSameGrid = false;
        if (targetCity != null && targetUnit != null)
            isSameGrid = SnapToGrid(targetCity.transform.position) == SnapToGrid(targetUnit.transform.position);
        GameObject target = null;
        GameObject nearestTarget;
        // GameObject[] enemiesList = GameObject.FindGameObjectsWithTag("Enemy");
        if (targetCity == null && targetUnit == null) {return;}
        else
        {
            if (isNearestTargetIsCity(enemy))
            {
                if (targetCity != null && IsEnemyAtPosition(targetCity.transform.position, enemy))
                {
                    GameObject newTargetUnit = FindNearestArmy(enemy);
                    GameObject newTargetCity = FindNearestCity(enemy);
                    float distUnit = newTargetUnit != null ? Vector3.Distance(enemy.transform.position, newTargetUnit.transform.position) : float.MaxValue;
                    float distCity = newTargetCity != null ? Vector3.Distance(enemy.transform.position, newTargetCity.transform.position) : float.MaxValue;
                    if (distUnit < distCity && newTargetUnit != null)
                    {
                        target = newTargetUnit;
                        isTargetIsCity = false;
                    }
                    else if (newTargetCity != null)
                    {
                        target = newTargetCity;
                        isTargetIsCity = true;
                    }
                    else if (newTargetUnit != null)  // Trường hợp chỉ có unit
                    {
                        target = newTargetUnit;
                        isTargetIsCity = false;
                    }
                    else
                    {
                        // Không còn mục tiêu nào
                        return;
                    }
                }
                else
                {
                    target = targetCity;
                    isTargetIsCity = true;
                }
                    
            }
            else
            {
                target = targetUnit;
                isTargetIsCity = false;
            }
        }
        if (target == null) return;
        Vector3 moveToPos;
        if (IsTargetInRange(enemy, target))
        {
            if (isTargetIsCity)
            {
                if (cityAdj.cityHp > 0  && isSameGrid)
                {
                    enemyUnit.Attack(targetUnit);
                    cityAdj.TakeDamage(enemyUnit.TotalDame(), target);
                    enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
                    return;
                    
                }
                if ( cityAdj.cityHp>0 && !isSameGrid)
                {
                    cityAdj.TakeDamage(enemyUnit.TotalDame(), target);
                    enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
                    return;
                }
                else
                {
                    moveToPos = FindBestMovePosition(enemy, targetCity);
                }
            }
            else if (!isTargetIsCity)
            {
                enemyUnit.Attack(targetUnit);
                enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
                return;
            }
        }

        moveToPos = FindBestMovePosition(enemy, target);

        // Nếu tìm được vị trí để di chuyển
        if (moveToPos != enemy.transform.position)
        {
            enemyUnit.Move(moveToPos);
        }

        // Sau khi di chuyển, nếu đã vào tầm thì tấn công
        if (IsTargetInRange(enemy, target))
        {
            if (isTargetIsCity)
            {
                if (cityAdj.cityHp > 0 && isSameGrid)
                {
                    enemyUnit.Attack(targetUnit);
                    targetCity.GetComponent<ClassCity>().TakeDamage(enemyUnit.TotalDame(), target);
                    enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
                    return;

                }

                if (cityAdj.cityHp > 0 && !isSameGrid)
                {
                    targetCity.GetComponent<ClassCity>().TakeDamage(enemyUnit.TotalDame(), target);
                    enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
                    return;
                }
            }
            else
            {
                enemyUnit.Attack(target);
                enemyUnit.CurrentSpeed = Mathf.Max(0, enemyUnit.CurrentSpeed - 1);
            }
            
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
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("City"))
        {
            ClassCity city = go.GetComponent<ClassCity>();
            if (city.cityHp > 0 && city.isPlayerCity)
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
        Vector3 targetPos = target.transform.position;

        foreach (var pos in possibleMoves)
        {
            float dist = Vector3.Distance(pos, targetPos);
            if (dist < minDistance)
            {
                minDistance = dist;
                bestPos = pos;
            }
        }

        return bestPos;
    }
    private static bool IsEnemyAtPosition(Vector3 position, GameObject currentEnemy)
    {
        Vector3 snapPos = SnapToGrid(position);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (enemy != currentEnemy)
            {
                Vector3 enemyPos = SnapToGrid(enemy.transform.position);
                if (enemyPos == snapPos)
                    return true;
            }
        }
        return false;
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