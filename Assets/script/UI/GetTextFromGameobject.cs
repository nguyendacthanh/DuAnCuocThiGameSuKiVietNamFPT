using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetTextFromGameobjectName : MonoBehaviour
{
    public GameObject targetGameObject;
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        if (targetGameObject != null && textMeshPro != null)
        {
            textMeshPro.text = targetGameObject.name;
        }
    }
}
