using UnityEngine;

public abstract class BuildingClass : MonoBehaviour
{
    public string buildingName;
    public int buildingCost;

    public abstract void BuildingAbility(ClassCity city);
}

public class InComBuilding : BuildingClass
{
    public override void BuildingAbility(ClassCity city)
    {
        Debug.Log(city.cityName + " xây Income Building");
    }
}

public class DefenseBuilding : BuildingClass
{
    public override void BuildingAbility(ClassCity city)
    {
        Debug.Log(city.cityName + " xây Defense Building");
    }
}

public class MilitaryBuilding : BuildingClass
{
    public GameObject armyPrefab;
    public int soLuongArmy = 2;

    public override void BuildingAbility(ClassCity city)
    {
        for (int i = 0; i < soLuongArmy; i++)
        {
            Vector3 pos = city.transform.position + new Vector3(i * 1.5f, 0, 0);
            GameObject.Instantiate(armyPrefab, pos, Quaternion.identity);
        }
    }
}
