using UnityEngine;

public class KyBinh : classDonVi
{
    void Awake()
    {
        Init(
            ten: "Đại Việt kỵ binh",
            loai: "cavalry",
            quan: "spear",
            cong: 30,
            hp: 200,
            giap: 15,
            xk: 25,
            tocdo: 8,
            kl: 20,
            luotDi: 1,
            luotAtk: 1,
            tam: 1
        );
    }
}
