using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameTurnManager : MonoBehaviour
{
    public enum Turn { Player, Enemy }
    public Turn currentTurn = Turn.Player;

    public Button nutKetThucLuot; // Gán trong Inspector (hoặc tìm runtime)
    private int soLuot = 1;

    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();

        if (nutKetThucLuot == null)
        {
            GameObject buttonObj = GameObject.Find("ButtonKetThucLuot");
            if (buttonObj != null)
                nutKetThucLuot = buttonObj.GetComponent<Button>();
        }

        if (nutKetThucLuot != null)
            nutKetThucLuot.onClick.AddListener(ChuyenLuot);
        else
            Debug.LogWarning("Chưa gán Button Kết Thúc Lượt");

        CapNhatLuatChoTatCaDonVi();
    }

    private void ChuyenLuot()
    {
        if (currentTurn == Turn.Player)
        {
            currentTurn = Turn.Enemy;
            CapNhatLuatChoTatCaDonVi();

            if (enemyManager != null)
                enemyManager.BatDauLuotEnemy();  
        }
        else
        {
            currentTurn = Turn.Player;
            soLuot++;
            CapNhatLuatChoTatCaDonVi();
        }

        Debug.Log("Lượt hiện tại: " + currentTurn + " | Số lượt: " + soLuot);
    }

    private void CapNhatLuatChoTatCaDonVi()
    {
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in armies)
        {
            ClassDonVi dv = go.GetComponent<ClassDonVi>();
            if (dv != null)
            {
                if (currentTurn == Turn.Player)
                    dv.KhoiPhucLuot();
                else
                    dv.ResetLuot();
            }
        }

        foreach (GameObject go in enemies)
        {
            ClassDonVi dv = go.GetComponent<ClassDonVi>();
            if (dv != null)
            {
                if (currentTurn == Turn.Enemy)
                    dv.KhoiPhucLuot();
                else
                    dv.ResetLuot();
            }
        }
    }

    public void ChuyenSangLuotNguoiChoi()
    {
        currentTurn = Turn.Player;
        soLuot++;
        
        CapNhatLuatChoTatCaDonVi();
    }
}