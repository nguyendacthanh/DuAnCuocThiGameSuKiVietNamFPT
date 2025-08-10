using UnityEngine;

public class Level2 : MonoBehaviour
{
    public GameObject win, lose;
    private GameObject[] enemies;
    private GameObject[] cities;
    private GameObject[] player;
    public GameObject City; // City để kiểm tra
    private GameTurnManager turnManager; // để lấy số lượt

    void Start()
    {
        turnManager = FindObjectOfType<GameTurnManager>();
    }

    void Update()
    {
        // Kiểm tra nếu City không phải là city của người chơi => thua
        if (City != null)
        {
            ClassCity info = City.GetComponent<ClassCity>();

            if (info != null)
            {
                if (!info.isPlayerCity)
                {
                    lose.SetActive(true);
                    win.SetActive(false);
                    return; // Không cần kiểm tra tiếp
                }

                // Nếu là city của player và số lượt = 11 => thắng
                if (info.isPlayerCity && turnManager != null && turnManager.CurrenTurn() == 11)
                {
                    win.SetActive(true);
                    lose.SetActive(false);
                    return; // Không cần kiểm tra tiếp
                }
            }
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        cities = GameObject.FindGameObjectsWithTag("City");
        player = GameObject.FindGameObjectsWithTag("Player");

        int nonPlayerCities = 0;
        int PlayerCities = 0;

        foreach (GameObject city in cities)
        {
            ClassCity info = city.GetComponent<ClassCity>();
            if (info != null && !info.isPlayerCity)
            {
                nonPlayerCities++;
            }
            else if (info != null && info.isPlayerCity)
            {
                PlayerCities++;
            }
        }

        if (enemies.Length == 0 && nonPlayerCities == 0)
        {
            win.SetActive(true);
            lose.SetActive(false);
        }
        else if (player.Length == 0 && PlayerCities == 0)
        {
            lose.SetActive(true);
            win.SetActive(false);
        }
    }
}
