using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject prefabSatThuong; // Gán prefab từ Inspector
    private classDonVi enemy;

    private void Awake()
    {
        enemy = GetComponent<classDonVi>();
    }

    public void ThucHienHanhDong()
    {
        DiChuyen();
        TanCong();
    }

    public void DiChuyen()
    {
        Vector3 mucTieu = AiMove.FindNearestArmy(enemy.gameObject);
        if (mucTieu == null) return;

        Vector3 viTriDen = AiMove.MovePosition(enemy.gameObject, mucTieu);
        AiMove.EnemyMove(enemy.gameObject, viTriDen);
    }

    public void TanCong()
    {
        AiAttack.TanCong(enemy.gameObject, prefabSatThuong); // Truyền prefab từ Inspector
    }
}