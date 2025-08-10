using UnityEngine;
using UnityEngine.UI;

public class GeneralUpgradeUI : MonoBehaviour
{
    public GeneralData general;

    public void UpgradeAtk(int value) => general.baseAtk += value;
    public void UpgradeDef(int value) => general.baseDef += value;
    public void UpgradeHp(int value) => general.baseHp += value;
    public void UpgradeCharge(int value) => general.baseCharge += value;
    public void UpgradeSpeed(int value) => general.baseSpeed += value;
    public void UpgradeMass(int value) => general.baseMass += value;
}