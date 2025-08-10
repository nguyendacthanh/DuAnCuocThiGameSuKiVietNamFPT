using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject win, lose;
    private GameObject[] enemies;
    private GameObject[] cities;
    private GameObject[] player;

    void Update()
    {
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
            lose.SetActive(true);
            win.SetActive(false);
        }
        else if (player.Length == 0 && PlayerCities == 0)
        {
            win.SetActive(true);
            lose.SetActive(false);
        }
    }
}