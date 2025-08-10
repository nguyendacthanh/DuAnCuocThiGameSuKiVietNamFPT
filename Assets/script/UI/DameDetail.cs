using TMPro;
using UnityEngine;

public class DameDetail : MonoBehaviour
{
    private float speed = 50f;           // Tốc độ bay
    public TextMeshProUGUI textMesh;          // TextMesh hiển thị damage

    void Start()
    {
        // startPos = transform.position;
        Destroy(gameObject, 1f);
    }

    void Update()
    {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    // Gọi từ ClassUnit để truyền damage
    public void SetDamageText(int damage)
    {
        if (textMesh != null)
        {
            textMesh.text = damage.ToString();
        }
    }
}
