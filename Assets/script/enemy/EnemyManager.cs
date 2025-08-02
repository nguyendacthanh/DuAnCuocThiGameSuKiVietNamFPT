using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public float delayGiuaCacEnemy = 0.5f;

    private GameTurnManager gameTurnManager;

    private void Start()
    {
        gameTurnManager = FindAnyObjectByType<GameTurnManager>();
    }

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
                ai.ThucHienHanhDong();
                yield return new WaitForSeconds(delayGiuaCacEnemy);
            }
        }

        // Sau khi tất cả enemy đã xong hành động, chuyển về PlayerTurn
        if (gameTurnManager != null)
        {
            gameTurnManager.ChuyenSangLuotNguoiChoi();
        }
    }
    
}