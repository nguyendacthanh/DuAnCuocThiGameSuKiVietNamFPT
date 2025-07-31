using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public GameObject prefabGridEnemy, prefabGridChon, prefabGridDiChuyen, prefabGridAttack,satThuongPrefab;
    private actions action;

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
            HandleClick();
        }
    }

    void HandleClick()
    {
        Debug.Log("ClickManager xử lý click (chỉ khi KHÔNG click vào button)");
        // Ví dụ: chọn vật thể, mở menu, v.v.
    }
}
