using UnityEngine;
using UnityEngine.UI;

public class SliderHpOfUnit : MonoBehaviour
{
    private Slider slider;
    private ClassUnit unit;
    private UnitData unitData;

    void Start()
    {
        slider = GetComponent<Slider>();
        unit = GetComponentInParent<ClassUnit>();  // Tự tìm ClassUnit từ cha

        if (unit != null && slider != null)
        {
            slider.minValue = 0;
            slider.maxValue = unit.unitData.Hp;  // Lấy từ unitData
            slider.value = unit.CurrentHp;
        }
    }

    void Update()
    {
        if (unit != null && slider != null)
        {
            slider.value = unit.CurrentHp;
        }
    }
}
