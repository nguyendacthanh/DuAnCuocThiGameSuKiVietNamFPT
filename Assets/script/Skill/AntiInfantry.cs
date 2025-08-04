using Unity.VisualScripting;
using UnityEngine;

public class AntiInfantry : ClassSkill
{
    protected override void Start()
    {
        skillName = "AntiInfantry";
        skillType = "passive";
    }
    public override void TriggerEffect(GameObject thisArmy, GameObject target)
    {
        var armyAttacker = thisArmy.GetComponent<ClassDonVi>();
        var armyAttacked = target.GetComponent<ClassDonVi>();
        if (armyAttacked != null && armyAttacked.BranchArmy == "Infantry")
        {
            armyAttacker.totalDame *= 200;
        }
        
    }
}
