using UnityEngine;
using UnityEngine.UI;

public class StatsArmy : MonoBehaviour
{
    public Text text;
    
    void Update()
    {
        ClassDonVi selectedUnit = FindSelectedUnit();
        if (selectedUnit != null)
        {
            text.text = "" + selectedUnit.NameArmy.ToString();
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
