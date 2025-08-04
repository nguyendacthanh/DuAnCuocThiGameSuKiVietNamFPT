using UnityEngine;

public class AntiCharge : ClassSkill
    
{
    protected override void Start()
    {
        skillName = "AntiCharge";
        skillType = "passive";
    }
    public override void TriggerEffect(GameObject thisArmy, GameObject target)
    {
        var armyAttacker = thisArmy.GetComponent<ClassDonVi>();
        var armyAttacked = target.GetComponent<ClassDonVi>();
        if (armyAttacker != null && armyAttacker.BranchArmy == "Calvary")
        {
            armyAttacked.Mass += (armyAttacker.Charge*armyAttacker.NumberBlock)/2;
            armyAttacker.Charge = 0;
            
        }
        
    }
}
