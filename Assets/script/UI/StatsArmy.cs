using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsArmy : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        ClassUnit selectedUnit = FindSelectedUnit();
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
                text.text = "Type: " + selectedUnit.TypeUnit;
            else if (objectName == "Hp")
                text.text = "HP: " + selectedUnit.CurrentHp.ToString();
            else if (objectName == "Defense")
                text.text = "Defense: " + selectedUnit.Def.ToString();
            else if (objectName == "ChargeDame")
                text.text = "Charge Dame: " + selectedUnit.Charge.ToString();
            else if (objectName == "Mass")
                text.text = "Mass: " + selectedUnit.Mass.ToString();
            else if (objectName == "Brand")
                text.text = "Branch: " + selectedUnit.BranchUnit;
            else if (objectName == "NameArmy")
                text.text = "tên đơn vị: " + selectedUnit.NameUnit;
            else if (objectName == "Avatar")  // ⚠️ chỉ xử lý sprite nếu là Avatar
            {
                Image image = GetComponent<Image>();
                SpriteRenderer sr = selectedUnit.GetComponent<SpriteRenderer>();

                if (image != null && sr != null)
                {
                    image.sprite = sr.sprite;
                }
            }
            else
                text.text = ""; // Không rõ tên thì để trống
        }
        else
        {
            text.text = "";
        }
    }

    ClassUnit FindSelectedUnit()
    {
        ClassUnit[] units = FindObjectsOfType<ClassUnit>();
        foreach (var unit in units)
        {
            if (unit.isSelected)
                return unit;
        }

        return null;
    }
}