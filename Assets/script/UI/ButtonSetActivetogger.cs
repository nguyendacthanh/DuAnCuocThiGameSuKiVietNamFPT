using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetActive : MonoBehaviour
{
    public GameObject targetTrue;
    // public GameObject targetFalse;
    public void setActiveTrue()
    {
        targetTrue.SetActive(true);
    }
    // public void BatTattargetObject()
    // {
    //     bool currentState = targetTogger.activeSelf;
    //     targetTogger.SetActive(!currentState);
    // }
}