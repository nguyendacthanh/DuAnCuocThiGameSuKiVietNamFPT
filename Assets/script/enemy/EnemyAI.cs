using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private ClassUnit armyEnemy;
    private ClassUnit armyPlayer;
    private float time = 1f;

    private void Awake()
    {
        armyEnemy = GetComponent<ClassUnit>();
       
    }

    // public void ThucHienHanhDong()
    // {
    //     DiChuyen();
    // }
    public IEnumerator ThucHienHanhDong()
    {
        DiChuyen();
        yield return new WaitForSeconds(time);
    }
    public void DiChuyen()
    {
            AIAction.EnemyMove(armyEnemy.gameObject);
    }
    // private IEnumerator ChayLuotEnemy()
    // {
    //     yield return new WaitForSeconds(time);
    // }


}