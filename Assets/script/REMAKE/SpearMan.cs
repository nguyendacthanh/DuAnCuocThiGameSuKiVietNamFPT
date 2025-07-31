using UnityEngine;

public class SpearMan : ClassDonVi
{
    void Start()
    {
        NameArmy = "Đại Việt: Bộ Binh Giáo";
        TypeArmy = "Spearman";
        BranchArmy = "Infantry";
        Atk = 25;
        Def = 20;
        Hp = 250;
        Mass = 3;
        RangeAtk = 1;
        Charge = 3;
        Speed = 3;
        MaxTurnSpeed = 1;
        CurrentSpeed = MaxTurnSpeed;
        MaxTurnAtk = 1;
        CurrentAtk = MaxTurnAtk;
    }
}
