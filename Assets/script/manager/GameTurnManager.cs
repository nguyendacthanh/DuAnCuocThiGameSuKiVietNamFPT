using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameTurnManager : MonoBehaviour
{
    public enum Turn { Player, Enemy }
    public Turn currentTurn = Turn.Player;

    public Button nutKetThucLuot; // Gán trong Inspector
    private int soLuot = 1;

    private void Start()
    {
        nutKetThucLuot.onClick.AddListener(ChuyenLuot);
        CapNhatLuatChoTatCaDonVi();
    }

    private void ChuyenLuot()
    {
        if (currentTurn == Turn.Player)
        {
            currentTurn = Turn.Enemy;
        }
        else
        {
            currentTurn = Turn.Player;
            soLuot++;
        }

        Debug.Log("Lượt hiện tại: " + currentTurn + " | Số lượt: " + soLuot);
        CapNhatLuatChoTatCaDonVi();
    }

    private void CapNhatLuatChoTatCaDonVi()
    {
        // Tìm tất cả đơn vị có tag Army và Enemy
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Áp dụng cho Army
        foreach (GameObject go in armies)
        {
            classDonVi dv = go.GetComponent<classDonVi>();
            if (dv != null)
            {
                if (currentTurn == Turn.Player)
                    dv.KhoiPhucLuot();
                else
                    dv.ResetLuot();
            }
        }

        // Áp dụng cho Enemy
        foreach (GameObject go in enemies)
        {
            classDonVi dv = go.GetComponent<classDonVi>();
            if (dv != null)
            {
                if (currentTurn == Turn.Enemy)
                    dv.KhoiPhucLuot();
                else
                    dv.ResetLuot();
            }
        }
    }
}
