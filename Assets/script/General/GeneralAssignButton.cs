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
                        // Gán tướng
                        GameObject generalGO = Instantiate(generalPrefab, unit.transform);
                        generalGO.tag = "General";
                        
                        // Cộng chỉ số
                        classUnit.Atk += general.baseAtk;
                        classUnit.Def += general.baseDef;
                        classUnit.Hp += general.baseHp;
                        classUnit.CurrentHp += general.baseHp; // Có thể thêm theo nhu cầu
                        classUnit.Charge += general.baseCharge;
                        classUnit.Speed += general.baseSpeed;
                        classUnit.Mass += general.baseMass;

                        GeneralManager.Instance.DecreaseGeneralQuantity(general);
                        break;
                    }
                }
            }
        }

        // Nếu không còn tướng nào => ẩn hoặc xóa nút
        if (!GeneralManager.Instance.HasAvailableGeneral())
        {
            assignButton.gameObject.SetActive(false);
            // Destroy(assignButton.gameObject); // nếu muốn hủy luôn
        }
    }
}
