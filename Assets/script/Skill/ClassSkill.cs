using UnityEngine;

public class ClassSkill : MonoBehaviour
{
    public SkillData skillData;

    public void TriggerEffect(GameObject thisArmy, GameObject target)
    {
        if (skillData == null) return;

        switch (skillData.skillName)
        {
            case "AntiInfantry":
                AntiInfantry(thisArmy, target);
                break;
            case "AntiCavalry":
                AntiCavalry(thisArmy, target);
                break;
            case "AntiCharge":
                AntiCharge(thisArmy, target);
                break;
            default:
                Debug.LogWarning("Skill not recognized: " + skillData.skillName);
                break;
        }
    }

    void AntiInfantry(GameObject thisArmy, GameObject target)
    {
        var UnitAttacker = thisArmy.GetComponent<ClassUnit>();
        var UnitAttacked = target.GetComponent<ClassUnit>();

        if (UnitAttacked != null && UnitAttacked.unitData.BranchArmy == "Infantry")
        {
            UnitAttacker.totalDame += 100;
        }
    }

    void AntiCavalry(GameObject thisArmy, GameObject target)
    {
        var UnitAttacker = thisArmy.GetComponent<ClassUnit>();
        var UnitAttacked = target.GetComponent<ClassUnit>();

        if (UnitAttacked != null && UnitAttacked.unitData.BranchArmy == "Cavalry")
        {
            UnitAttacker.totalDame += 100;
        }
    }

    void AntiCharge(GameObject thisArmy, GameObject target)
    {
        var UnitAttacker = thisArmy.GetComponent<ClassUnit>();
        var UnitAttacked = target.GetComponent<ClassUnit>();

        if (UnitAttacker != null && UnitAttacker.unitData.BranchArmy == "Cavalry")
        {
            UnitAttacked.Mass += UnitAttacker.totalDame;
        }
    }
}