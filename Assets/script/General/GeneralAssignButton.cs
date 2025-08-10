using UnityEngine;
using UnityEngine.UI;

public class GeneralAssignButton : MonoBehaviour
{
    public Button assignButton;
    public GameObject generalPrefab; // Prefab đại diện tướng trong game
    public Transform generalParent;

    void Update()
    {
        if (assignButton == null || GeneralManager.Instance == null) return;

        bool showButton = false;

        foreach (var unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            var classUnit = unit.GetComponent<ClassUnit>();
            if (classUnit != null && classUnit.isSelected)
            {
                // ✅ Kiểm tra unit đã có tướng chưa
                bool hasGeneral = false;
                foreach (Transform child in unit.transform)
                {
                    if (child.CompareTag("General"))
                    {
                        hasGeneral = true;
                        break;
                    }
                }

                // ✅ Chỉ hiển thị nút nếu unit đang chọn chưa có tướng và còn tướng
                if (!hasGeneral && GeneralManager.Instance.HasAvailableGeneral())
                {
                    showButton = true;
                    break;
                }
            }
        }

        assignButton.gameObject.SetActive(showButton);
    }


    public void OnAssignGeneral()
    {
        foreach (var unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            var classUnit = unit.GetComponent<ClassUnit>();
            if (classUnit != null && classUnit.isSelected)
            {
                // ✅ Nếu unit đã có tướng, bỏ qua
                foreach (Transform child in unit.transform)
                {
                    if (child.CompareTag("General"))
                    {
                        Debug.Log("Unit đã có tướng, không thể gán thêm.");
                        return;
                    }
                }

                // ✅ Tìm tướng còn quantity > 0
                foreach (var general in GeneralManager.Instance.generals)
                {
                    if (general.quantity > 0)
                    {
                        // Tạo tướng ở vị trí unit, làm con của unit
                        GameObject generalGO = Instantiate(
                            generalPrefab,
                            unit.transform.position,
                            Quaternion.identity,
                            unit.transform
                        );

                        generalGO.tag = "General";

                        // Cộng chỉ số tướng
                        classUnit.Atk += general.baseAtk;
                        classUnit.Def += general.baseDef;
                        classUnit.Hp += general.baseHp;
                        classUnit.CurrentHp += general.baseHp;
                        classUnit.Charge += general.baseCharge;
                        classUnit.Speed += general.baseSpeed;
                        classUnit.Mass += general.baseMass;

                        // Giảm số lượng tướng
                        GeneralManager.Instance.DecreaseGeneralQuantity(general);

                        Debug.Log("Đã gán tướng và sẽ xóa nút.");

                        // ❌ XÓA nút sau khi gán tướng
                        Destroy(assignButton.gameObject);

                        return; // Thoát sau khi gán 1 tướng
                    }
                }

                Debug.Log("Không còn tướng nào để gán.");
            }
        }
    }



}
