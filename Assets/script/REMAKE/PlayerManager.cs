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
            // ✅ Nếu đang click vào UI (ví dụ button), thì bỏ qua hoàn toàn
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // ✅ Nếu không click vào UI, mới xử lý logic click của ClickManager
            action.ClickEvent(satThuongPrefab,prefabGridEnemy, prefabGridDiChuyen,prefabGridAttack,prefabGridChon,buttonInformation);
        }
    }

}
