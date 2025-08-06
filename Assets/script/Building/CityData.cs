using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Cities/CityData")]

public class CityData : ScriptableObject
{
    public string cityName;
    public int cityHp;
    public int population,incom;
    public bool isPlayerCity;
}
