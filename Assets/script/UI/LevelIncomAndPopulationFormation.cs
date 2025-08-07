using TMPro;
using UnityEngine;

public class LevelIncomAndPopulationFormation : MonoBehaviour
{
    public PlayerManager playerManager;
    public TextMeshProUGUI textThongTin;

    private void Start()
    {
        if (playerManager == null)
            playerManager = FindAnyObjectByType<PlayerManager>();

        if (textThongTin == null)
            textThongTin = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (playerManager != null && playerManager.incomeSystem != null)
        {
            var incomeSys = playerManager.incomeSystem;

            textThongTin.text = $"Tổng tiền = {incomeSys.totalIncome} | Dân số = {incomeSys.population}/{incomeSys.totalPopulation}";
        }
    }
}
