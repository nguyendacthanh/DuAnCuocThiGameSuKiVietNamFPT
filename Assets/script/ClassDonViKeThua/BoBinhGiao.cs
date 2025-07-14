using UnityEngine;

public class BoBinhGiao : classDonVi
{
    void Awake()
    {
        Init(
            ten: "Đại Việt giáo binh",
            loai: "infantry",
            quan: "spear",
            cong: 30,
            hp: 400,
            giap: 40,
            xk: 5,
            tocdo: 4,
            kl: 5,
            luotDi: 1,
            luotAtk: 1,
            tam: 1
        );
    }
}
