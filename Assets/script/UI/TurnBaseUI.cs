using TMPro;
using UnityEngine;

public class TurnBaseUI : MonoBehaviour
{
    public TextMeshProUGUI textLuot; 
    private GameTurnManager turnManager;

    void Start()
    {
        turnManager = FindAnyObjectByType<GameTurnManager>();
    }
    void Update()
    {
        if (turnManager != null && textLuot != null)
        {
            textLuot.text = "Lượt: " + turnManager.CurrenTurn();
        }
    }

}
