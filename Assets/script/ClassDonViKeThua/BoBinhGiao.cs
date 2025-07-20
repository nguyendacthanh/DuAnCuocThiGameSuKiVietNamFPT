using UnityEngine;

public class BoBinhGiao : classDonVi
{
    void Start()
    {
        Debug.Log("Start gọi: BoBinhGiao");
        Init("Đại Việt giáo binh", "infantry", "spear", 30, 400, 40, 5, 4, 5, 1, 1, 1);
    }
}