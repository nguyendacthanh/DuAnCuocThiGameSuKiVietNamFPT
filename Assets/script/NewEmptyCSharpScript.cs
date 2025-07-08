using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // chuột trái
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(mousePos.x, mousePos.y); // 2D

            GameObject nearestArmy = FindNearestArmy(clickPos);

            if (nearestArmy != null)
            {
                Destroy(nearestArmy);
                Debug.Log("Đã xóa " + nearestArmy.name);
            }
        }
    }

    GameObject FindNearestArmy(Vector2 clickPos)
    {
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
        float minDist = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject army in armies)
        {
            float dist = Vector2.Distance(clickPos, army.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = army;
            }
        }

        return closest;
    }
}
