using UnityEngine;

public class InComAndPopulation 
{
    public int startingIncome = 100;
    public int totalIncome;
    public int totalPopulation;
    public int incomPerturn;
    public int PopulationUp;
    public int population;

    public void Initialize()
    {
        totalIncome = startingIncome; 
    }
    public void UpdateIncomeAndPopulation()
    {
        totalIncome += incomPerturn; 
    }
    public void RecalculateIncomeAndPopulationFromCities()
    {
        totalPopulation = 0;
        incomPerturn = 0;
        population = 0;

        GameObject[] cities = GameObject.FindGameObjectsWithTag("City");
        foreach (GameObject cityObj in cities)
        {
            ClassCity city = cityObj.GetComponent<ClassCity>();
            if (city != null && city.isPlayerCity)
            {
                incomPerturn += city.incom;
                totalPopulation += city.population;
            }
        }

        GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject unitObj in units)
        {
            if (unitObj.GetComponent<ClassUnit>() != null)
            {
                population += 1;
            }
        }
    }

}
