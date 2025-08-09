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
    
    public float delayGiuaCacEnemy = 0.5f;

    private GameTurnManager gameTurnManager;
    public void BatDauLuotEnemy()
    {
        Capture();
        StartCoroutine(ChayLuotEnemy());
    }
    public void BatDauLuotPlayer()
    {
        incomeSystem.RecalculateIncomeAndPopulationFromCities(); // Đếm lại tất cả City
        incomeSystem.UpdateIncomeAndPopulation();               // Cộng income theo lượt
        Capture();
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



    
    public void Capture()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] city = GameObject.FindGameObjectsWithTag("City");
        foreach (var cities in city)
        {
            foreach (var p in player)
            {
                if (cities.transform.position == p.transform.position)
                {
                    cities.GetComponent<ClassCity>().isPlayerCity = true;
                }
            }

            foreach (var e in enemy)
            {
                if (cities.transform.position == e.transform.position)
                {
                    cities.GetComponent<ClassCity>().isPlayerCity = false;

                }
            }
        }
    }
}
