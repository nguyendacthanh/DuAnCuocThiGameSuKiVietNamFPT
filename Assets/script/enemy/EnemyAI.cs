using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private classDonVi enemy;

    private void Awake()
    {
        enemy = GetComponent<classDonVi>();
    }

    public void ThucHienHanhDong()
    {
        DiChuyen();
    }

    public void DiChuyen()
    {
        Vector3 mucTieu = AiMove.FindNearestArmy(enemy.gameObject);
        if (mucTieu == null) return;

        Vector3 viTriDen = AiMove.MovePosition(enemy.gameObject, mucTieu);
        AiMove.EnemyMove(enemy.gameObject, viTriDen);
    }
}