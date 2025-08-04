using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsArmy : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        ClassDonVi selectedUnit = FindSelectedUnit();
        if (selectedUnit != null)
        {
            string objectName = gameObject.name;

            if (objectName == "AttackDame")
                text.text = "Attack: " + selectedUnit.Atk.ToString();
            else if (objectName == "Speed")
                text.text = "Speed: " + selectedUnit.Speed.ToString();
            else if (objectName == "Range")
                text.text = "Range: " + selectedUnit.RangeAtk.ToString();
            else if (objectName == "Type")
                text.text = "Type: " + selectedUnit.TypeArmy;
            else if (objectName == "Hp")
                text.text = "HP: " + selectedUnit.Hp.ToString();
            else if (objectName == "Defense")
                text.text = "Defense: " + selectedUnit.Def.ToString();
            else if (objectName == "ChargeDame")
                text.text = "Charge Dame: " + selectedUnit.Charge.ToString();
            else if (objectName == "Mass")
                text.text = "Mass: " + selectedUnit.Mass.ToString();
            else if (objectName == "Brand")
                text.text = "Branch: " + selectedUnit.BranchArmy;
            else if (objectName == "NameArmy")
                text.text = "tên đơn vị: " + selectedUnit.NameArmy;
            else
                text.text = ""; // Không rõ tên thì để trống
        }
        else
        {
            text.text = "";
        }
    }

    ClassDonVi FindSelectedUnit()
    {
        ClassDonVi[] units = FindObjectsOfType<ClassDonVi>();
        foreach (var unit in units)
        {
            if (unit.isSelected)
                return unit;
        }

        return null;
    }
}