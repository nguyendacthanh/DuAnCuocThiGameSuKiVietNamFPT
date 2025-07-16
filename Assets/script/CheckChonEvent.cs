using System.Collections.Generic;
using UnityEngine;

public class CheckChonEvent
{
    public List<Vector2> ToaDoArmy { get; private set; }
    public List<Vector2> ToaDoEnemy { get; private set; }

    public List<GameObject> armies { get; private set; } = new List<GameObject>();
    public List<GameObject> enemies { get; private set; } = new List<GameObject>();

    public bool EnemyChosen { get; private set; } = false;
    public bool ArmyChosen { get; private set; } = false;

    public void CapNhatToaDo()
    {
        ToaDoArmy = new List<Vector2>();
        ToaDoEnemy = new List<Vector2>();
        armies = new List<GameObject>();
        enemies = new List<GameObject>();

        GameObject[] armyObjs = GameObject.FindGameObjectsWithTag("Army");
        GameObject[] enemyObjs = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject a in armyObjs)
        {
            ToaDoArmy.Add((Vector2)a.transform.position);
            armies.Add(a);
        }

        foreach (GameObject e in enemyObjs)
        {
            ToaDoEnemy.Add((Vector2)e.transform.position);
            enemies.Add(e);
        }
    }

    public bool TimToaDoEnemy(Vector2 clickPos)
    {
        foreach (Vector2 pos in ToaDoEnemy)
        {
            if (pos == clickPos)
            {
                EnemyChosen = true;
                return true;
            }
        }

        EnemyChosen = false;
        return false;
    }

    public bool TimToaDoArmy(Vector2 clickPos)
    {
        foreach (Vector2 pos in ToaDoArmy)
        {
            if (pos == clickPos)
            {
                ArmyChosen = true;
                return true;
            }
        }

        ArmyChosen = false;
        return false;
    }
}
