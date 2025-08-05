using UnityEngine;

[CreateAssetMenu(menuName = "Units/UnitData")]
public class UnitData : ScriptableObject
{
    public string NameArmy;
    public string TypeArmy;
    public string BranchArmy;

    public int Cost;
    public int Population;
    public int Atk;
    public int Def;
    public int Hp;
    public int Charge;
    public int Speed;
    public int RangeAtk;
    public int Mass;
    public int MaxTurnSpeed;
    public int MaxTurnAtk;
}