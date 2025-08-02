using UnityEngine;

public class AIAction
{
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
}
