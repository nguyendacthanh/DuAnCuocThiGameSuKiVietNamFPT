using UnityEngine;

public class CungThu : classDonVi
{
    void Awake()
    {
        Init(
            ten: "Đại Việt cung binh",
            loai: "infantry",
            quan: "archer",
            cong: 95,
            hp: 180,
            giap: 10,
            xk: 2,
            tocdo: 3,
            kl: 2,
            luotDi: 1,
            luotAtk: 1,
            tam: 3 // tầm bắn xa
        );
    }
}
