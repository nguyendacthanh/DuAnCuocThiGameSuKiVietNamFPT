using UnityEngine;

public class ChonDonVi
{
    private float khoangCachGioiHan;

    public ChonDonVi(float khoangCach)
    {
        this.khoangCachGioiHan = khoangCach;
    }

    public GameObject TimDonViGanNhat(Vector2 clickPos)
    {
        GameObject[] donVis = GameObject.FindGameObjectsWithTag("Army"); // chỉ chọn đơn vị của người chơi
        GameObject donViGanNhat = null;
        float khoangCachNhoNhat = float.MaxValue;

        foreach (GameObject donVi in donVis)
        {
            float dist = Vector2.Distance(clickPos, donVi.transform.position);
            if (dist <= khoangCachGioiHan && dist < khoangCachNhoNhat)
            {
                khoangCachNhoNhat = dist;
                donViGanNhat = donVi;
            }
        }

        return donViGanNhat;
    }
}
