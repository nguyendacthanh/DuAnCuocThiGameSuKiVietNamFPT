using UnityEngine;

public class ChonDonVi
{
    private float khoangCachGioiHan;

    public ChonDonVi(float khoangCachGioiHan)
    {
        this.khoangCachGioiHan = khoangCachGioiHan;
    }

    public GameObject TimDonViGanNhat(Vector2 clickPos)
    {
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
        float minDist = Mathf.Infinity;
        GameObject nearest = null;

        foreach (GameObject army in armies)
        {
            Vector2 DonViPositionNearest = army.transform.position;
            float dx = Mathf.Abs(DonViPositionNearest.x - clickPos.x);
            float dy = Mathf.Abs(DonViPositionNearest.y - clickPos.y);

            if (dx <= khoangCachGioiHan && dy <= khoangCachGioiHan)
            {
                float dist = Vector2.Distance(clickPos, DonViPositionNearest);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = army;
                }
            }
        }

        return nearest;
    }
}
