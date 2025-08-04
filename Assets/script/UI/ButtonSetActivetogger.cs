using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetActive : MonoBehaviour
{
    public GameObject targetObject;
    public void BatTattargetObject()
    {
        bool currentState = targetObject.activeSelf;
        targetObject.SetActive(!currentState);
    }
}