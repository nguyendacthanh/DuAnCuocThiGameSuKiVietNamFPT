using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetActive : MonoBehaviour
{
    public GameObject targetObject;
    public void ShowObject()
    {
        Animator animator = targetObject.GetComponent<Animator>();
        targetObject.SetActive(true);
        animator.SetBool("OnClick", true);
    }

    public void HideObject()
    {
        Animator animator = targetObject.GetComponent<Animator>();

            targetObject.SetActive(false);
            Debug.Log("Đã tắt GameObject: " + targetObject.name);
            animator.SetBool("OnClick", true);


    }

    public void ToggleObject()
    {
        Animator animator = targetObject.GetComponent<Animator>();

            bool currentState = targetObject.activeSelf;
            targetObject.SetActive(!currentState);
            animator.SetBool("OnClick", true);


    }
}
