using UnityEngine;

public class click : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click chuột trái
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 viTriLamTron = ToaDoClick(mouseWorldPos);
            Debug.Log("Tọa độ click làm tròn: " + viTriLamTron);
        }
    }

    public Vector2 ToaDoClick(Vector2 clickPos)
    {
        float x = clickPos.x;
        float y = clickPos.y;

        float duX = Mathf.Abs(x % 100);
        float duY = Mathf.Abs(y % 100);

        float chinhX = (x >= 0)
            ? (duX >= 50 ? x - duX + 100 : x - duX)
            : (duX >= 50 ? x + duX - 100 : x + duX);

        float chinhY = (y >= 0)
            ? (duY >= 50 ? y - duY + 100 : y - duY)
            : (duY >= 50 ? y + duY - 100 : y + duY);

        return new Vector2(Mathf.Round(chinhX), Mathf.Round(chinhY));
    }
}
