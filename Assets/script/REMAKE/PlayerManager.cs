using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public GameObject prefabGridEnemy, prefabGridChon, prefabGridDiChuyen, prefabGridAttack;
    private actions action;
    public GameObject buttonInformation;
    public InComAndPopulation incomeSystem = new InComAndPopulation();
    private void Start()
    {
        action = new actions();
        gameTurnManager = FindAnyObjectByType<GameTurnManager>();
        incomeSystem.RecalculateIncomeAndPopulationFromCities();
        incomeSystem.Initialize();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            action.ClickEvent(prefabGridEnemy, prefabGridDiChuyen,prefabGridAttack,prefabGridChon,buttonInformation);
        }
    }
    
    public float delayGiuaCacEnemy = 1f;

    private GameTurnManager gameTurnManager;
    public void BatDauLuotEnemy()
    {
        StartCoroutine(ChayLuotEnemy());
    }
    public void BatDauLuotPlayer()
    {
        incomeSystem.RecalculateIncomeAndPopulationFromCities(); // Đếm lại tất cả City
        incomeSystem.UpdateIncomeAndPopulation();                // Cộng income theo lượt
    }
    private IEnumerator ChayLuotEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                // ai.ThucHienHanhDong();
                yield return StartCoroutine(ai.ThucHienHanhDong());
                yield return new WaitForSeconds(delayGiuaCacEnemy);
            }
        }
        // Sau khi tất cả enemy đã xong hành động, chuyển về PlayerTurn
            gameTurnManager.ChuyenSangLuotNguoiChoi();
    }

}
