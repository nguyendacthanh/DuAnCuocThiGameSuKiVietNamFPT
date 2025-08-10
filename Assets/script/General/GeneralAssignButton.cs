using UnityEngine;
using UnityEngine.UI;

public class GeneralAssignButton : MonoBehaviour
{
    public Button assignButton;
    public GameObject generalPrefab; // Prefab đại diện tướng trong game
    public Transform generalParent;

    void Update()
    {
        bool hasSelectedUnit = false;

        foreach (var unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            var classUnit = unit.GetComponent<ClassUnit>();
            if (classUnit != null && classUnit.isSelected)
            {
                hasSelectedUnit = true;
                break;
            }
        }

        assignButton.gameObject.SetActive(hasSelectedUnit && GeneralManager.Instance.HasAvailableGeneral());
    }

    public void OnAssignGeneral()
    {
        foreach (var unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            var classUnit = unit.GetComponent<ClassUnit>();
            if (classUnit != null && classUnit.isSelected)
            {
                // Lấy tướng đầu tiên còn quantity
                foreach (var general in GeneralManager.Instance.generals)
                {
                    if (general.quantity > 0)
                    {
                        // Instantiate prefab tại vị trí unit, làm con unit luôn
                        GameObject generalGO = Instantiate(generalPrefab, unit.transform.position, Quaternion.identity, unit.transform);

                        // Gán tag General cho prefab vừa tạo
                        generalGO.tag = "General";

                        // Cộng chỉ số tướng cho unit
                        classUnit.Atk += general.baseAtk;
                        classUnit.Def += general.baseDef;
                        classUnit.Hp += general.baseHp;
                        classUnit.CurrentHp += general.baseHp; // Cộng máu hiện tại
                        classUnit.Charge += general.baseCharge;
                        classUnit.Speed += general.baseSpeed;
                        classUnit.Mass += general.baseMass;

                        // Giảm số lượng tướng đi 1
                        GeneralManager.Instance.DecreaseGeneralQuantity(general);
                        break;
                    }
                }
            }
        }

        // Nếu không còn tướng nào => ẩn nút
        if (!GeneralManager.Instance.HasAvailableGeneral())
        {
            assignButton.gameObject.SetActive(false);
            // Hoặc Destroy(assignButton.gameObject); nếu muốn hủy nút luôn
        }
    }

}
