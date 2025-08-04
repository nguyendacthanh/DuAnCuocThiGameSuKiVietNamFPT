using Unity.VisualScripting;
using UnityEngine;

public class ClassSkill : MonoBehaviour
{
    protected string skillName;
    protected string skillType;
    protected virtual void Start()
    {
        skillName = "defaultSkill";
        skillType = "passive";
        
    }
    public virtual void TriggerEffect(GameObject thisArmy, GameObject target)
    {
        
        // Tùy skill cụ thể
    }
}
