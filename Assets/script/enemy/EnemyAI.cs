using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private ClassUnit armyEnemy;
    private ClassUnit armyPlayer;
    private float time = 0.5f;

    private void Awake()
    {
        armyEnemy = GetComponent<ClassUnit>();
       
    }
    public IEnumerator ThucHienHanhDong()
    {
        AIAction.EnemyMove(armyEnemy.gameObject);
        yield return new WaitForSeconds(time);
    }

}