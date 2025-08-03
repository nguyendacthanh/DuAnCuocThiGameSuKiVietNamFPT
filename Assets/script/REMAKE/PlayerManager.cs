using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public GameObject prefabGridEnemy, prefabGridChon, prefabGridDiChuyen, prefabGridAttack,satThuongPrefab;
    private actions action;
    public GameObject buttonInformation;

    private void Start()
    {
        action = new actions();
        gameTurnManager = FindAnyObjectByType<GameTurnManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            action.ClickEvent(satThuongPrefab,prefabGridEnemy, prefabGridDiChuyen,prefabGridAttack,prefabGridChon,buttonInformation);
        }
    }
    
    public float delayGiuaCacEnemy = 1f;

    private GameTurnManager gameTurnManager;
    public void BatDauLuotEnemy()
    {
        StartCoroutine(ChayLuotEnemy());
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
