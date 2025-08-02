using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public GameObject prefabGridEnemy, prefabGridChon, prefabGridDiChuyen, prefabGridAttack,satThuongPrefab;
    private actions action;
    public GameObject buttonInformation;

    private void Start()
    {
        action = new actions();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            
            action.ClickEvent(satThuongPrefab,prefabGridEnemy, prefabGridDiChuyen,prefabGridAttack,prefabGridChon,buttonInformation);
        }
    }

}
