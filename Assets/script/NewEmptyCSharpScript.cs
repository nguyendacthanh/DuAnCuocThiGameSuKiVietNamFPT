using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public float maxDistanceXY = 50f; // giới hạn theo trục x hoặc y

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // chuột trái
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(mousePos.x, mousePos.y);

            GameObject nearestArmy = FindNearestArmyWithinLimit(clickPos);

            if (nearestArmy != null)
            {
                Destroy(nearestArmy);
                Debug.Log("Đã xóa " + nearestArmy.name);
            }
            else
            {
                Debug.Log("Không có Army nào trong phạm vi 50 đơn vị.");
            }
        }
    }

    GameObject FindNearestArmyWithinLimit(Vector2 clickPos)
    {
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
        float minDist = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject army in armies)
        {
            Vector2 armyPos = army.transform.position;
            float dx = Mathf.Abs(armyPos.x - clickPos.x);
            float dy = Mathf.Abs(armyPos.y - clickPos.y);

            if (dx <= maxDistanceXY && dy <= maxDistanceXY)
            {
                float dist = Vector2.Distance(clickPos, armyPos);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = army;
                }
            }
        }

        return closest;
    }
}
