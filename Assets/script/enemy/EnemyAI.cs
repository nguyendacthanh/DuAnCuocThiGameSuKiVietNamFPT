using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject prefabSatThuong; // Gán prefab từ Inspector
    private ClassDonVi armyEnemy;
    private ClassDonVi armyPlayer;

    private void Awake()
    {
        armyEnemy = GetComponent<ClassDonVi>();
        armyPlayer = GetComponent<ClassDonVi>();
    }

    public void ThucHienHanhDong()
    {
        DiChuyen();
        TanCong();
    }

    public void DiChuyen()
    {
        Vector3 mucTieu = AiMove.FindNearestArmy(armyEnemy.gameObject);
        if (mucTieu == null) return;

        Vector3 viTriDen = AiMove.MovePosition(armyEnemy.gameObject, mucTieu);
        AiMove.EnemyMove(armyEnemy.gameObject, viTriDen);
    }

    public void TanCong()
    {
        AiAttack.TanCong(armyEnemy.gameObject, prefabSatThuong); // Truyền prefab từ Inspector
    }
}