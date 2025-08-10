using UnityEngine;

public class Level3 : MonoBehaviour
{
    public GameObject win, lose;
    private GameObject[] enemies;
    // private GameObject[] cities;
    private GameObject[] player;

    private int nonPlayerCities = 0;
    private int playerCities = 0;

    private int enemyKilledCount = 0;
    private int targetKills = 15;
    private int initialEnemyCount = 0;

    void Start()
    {
        initialEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // cities = GameObject.FindGameObjectsWithTag("City");
        player = GameObject.FindGameObjectsWithTag("Player");

        nonPlayerCities = 0;
        playerCities = 0;

        // foreach (GameObject city in cities)
        // {
        //     ClassCity info = city.GetComponent<ClassCity>();
        //     if (info != null && !info.isPlayerCity)
        //     {
        //         nonPlayerCities++;
        //     }
        //     else if (info != null && info.isPlayerCity)
        //     {
        //         playerCities++;
        //     }
        // }

        // Cập nhật số enemy đã chết
        enemyKilledCount = initialEnemyCount - enemies.Length;

        // Điều kiện thắng theo kill count
        if (enemyKilledCount >= targetKills)
        {
            win.SetActive(true);
            lose.SetActive(false);
            return;
        }

        // Điều kiện thắng/thua cũ
        if (enemies.Length == 0 && nonPlayerCities == 0)
        {
            win.SetActive(true);
            lose.SetActive(false);
        }
        else if (player.Length == 0 && playerCities == 0)
        {
            lose.SetActive(true);
            win.SetActive(false);
        }
    }
}