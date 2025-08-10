using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUpgradeUI : MonoBehaviour
{
    public GeneralData general;

    // Các text UI
    public TMP_Text atkText;
    public TMP_Text defText;
    public TMP_Text hpText;
    public TMP_Text chargeText;
    public TMP_Text speedText;
    public TMP_Text massText;

    private void Start()
    {
        UpdateUI(); // Cập nhật khi mới mở
    }

    public void UpgradeAtk(int value)
    {
        general.baseAtk += value;
        UpdateUI();
        ProgressManager.Instance.SaveGeneralStats(GeneralManager.Instance.generals);
    }

    public void UpgradeDef(int value)
    {
        general.baseDef += value;
        UpdateUI();
        ProgressManager.Instance.SaveGeneralStats(GeneralManager.Instance.generals);
    }

    public void UpgradeHp(int value)
    {
        general.baseHp += value;
        UpdateUI();
        ProgressManager.Instance.SaveGeneralStats(GeneralManager.Instance.generals);
    }

    public void UpgradeCharge(int value)
    {
        general.baseCharge += value;
        UpdateUI();
        ProgressManager.Instance.SaveGeneralStats(GeneralManager.Instance.generals);
    }

    public void UpgradeSpeed(int value)
    {
        general.baseSpeed += value;
        UpdateUI();
        ProgressManager.Instance.SaveGeneralStats(GeneralManager.Instance.generals);
    }

    public void UpgradeMass(int value)
    {
        general.baseMass += value;
        UpdateUI();
        ProgressManager.Instance.SaveGeneralStats(GeneralManager.Instance.generals);
    }

    public void UpdateUI()
    {
        atkText.text = "ATK: " + general.baseAtk;
        defText.text = "DEF: " + general.baseDef;
        hpText.text = "HP: " + general.baseHp;
        chargeText.text = "Charge: " + general.baseCharge;
        speedText.text = "Speed: " + general.baseSpeed;
        massText.text = "Mass: " + general.baseMass;
    }
}