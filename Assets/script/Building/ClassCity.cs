using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ClassCity : MonoBehaviour
{
    public CityData cityData;
    public bool isSelected;
    public string cityName;
    public int cityHp;
    public bool isPlayerCity;
    public TextMeshProUGUI textMeshPro;
    public List<BuildingClass> building;
    public int incom , population;
    private void Start()
    {
        if (cityData != null)
        {
            cityName = cityData.cityName;
            cityHp = cityData.cityHp;
            incom = cityData.incom;
            population = cityData.population;
            isPlayerCity = cityData.isPlayerCity;
        }
        textMeshPro.text = cityName;
    }
    public void TakeDamage(int damage, GameObject attacker)
    {
        cityHp -= damage;
        if (cityHp <= 0)
        {
            cityHp = 0;
        }
    }
}
