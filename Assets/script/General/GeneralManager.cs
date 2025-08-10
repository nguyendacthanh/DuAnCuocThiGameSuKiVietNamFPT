using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager Instance;

    public List<GeneralData> generals;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ResetAllGenerals();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetAllGenerals()
    {
        foreach (var general in generals)
        {
            general.ResetQuantity();
        }
    }

    public bool HasAvailableGeneral()
    {
        foreach (var general in generals)
        {
            if (general.quantity > 0) return true;
        }
        return false;
    }

    public void DecreaseGeneralQuantity(GeneralData general)
    {
        general.quantity = Mathf.Max(0, general.quantity - 1);
    }
}