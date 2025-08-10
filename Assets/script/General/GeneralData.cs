using UnityEngine;

[CreateAssetMenu(fileName = "GeneralData", menuName = "Game/GeneralData")]
public class GeneralData : ScriptableObject
{
    public string generalName;
    public int baseAtk, baseDef, baseHp, baseCharge, baseSpeed, baseMass;
    public int quantity = 1;

    public void ResetQuantity()
    {
        quantity = 1;
    }
}