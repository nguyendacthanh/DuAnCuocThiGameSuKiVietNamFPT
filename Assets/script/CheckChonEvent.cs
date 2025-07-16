using System.Collections.Generic;
using UnityEngine;

public class CheckChonEvent
{
    public List<Vector2> ToaDoArmy { get; private set; }
    public List<Vector2> ToaDoEnemy { get; private set; }

    public bool EnemyChosen { get; private set; } = false;
    public bool ArmyChosen { get; private set; } = false;

    public void CapNhatToaDo()
    {
        ToaDoArmy = new List<Vector2>();
        ToaDoEnemy = new List<Vector2>();

        GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject a in armies)
        {
            ToaDoArmy.Add((Vector2)a.transform.position);
        }

        foreach (GameObject e in enemies)
        {
            ToaDoEnemy.Add((Vector2)e.transform.position);
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
