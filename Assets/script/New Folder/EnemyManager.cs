using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private classDonVi enemy;

    void Start()
    {
        enemy = GetComponent<classDonVi>();
    }

    void Update()
    {
        if (enemy.LuotDiChuyen > 0 || enemy.LuotTanCong > 0)
        {
            GameObject armyGanNhat = TimArmyGanNhat();
            if (armyGanNhat != null)
            {
                float dist = Vector3.Distance(armyGanNhat.transform.position, transform.position);
                classDonVi mucTieu = armyGanNhat.GetComponent<classDonVi>();

                if (dist <= enemy.tamTanCong * 100)
                {
                    if (enemy.LuotTanCong > 0)
                    {
                        enemy.TanCong(mucTieu);
                        enemy.LuotTanCong--;
                    }
                }
                else if (enemy.LuotDiChuyen > 0)
                {
                    enemy.DiChuyenDen(armyGanNhat.transform.position);
                }
            }
        }
    }

    GameObject TimArmyGanNhat()
    {
        GameObject[] armys = GameObject.FindGameObjectsWithTag("Army");
        float minDist = float.MaxValue;
        GameObject ganNhat = null;

        foreach (GameObject army in armys)
        {
            float dist = Vector3.Distance(army.transform.position, transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                ganNhat = army;
            }
        }

        return ganNhat;
    }
}
