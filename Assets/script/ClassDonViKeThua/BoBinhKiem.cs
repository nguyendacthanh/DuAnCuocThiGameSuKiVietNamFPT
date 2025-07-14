using UnityEngine;

public class BoBinhKiem : classDonVi
{
    void Awake()
    {
        Init(
            ten: "Đại Việt kiếm binh",
            loai: "infantry",
            quan: "sword",
            cong: 60,
            hp: 350,
            giap: 25,
            xk: 10,
            tocdo: 4,
            kl: 5,
            luotDi: 1,
            luotAtk: 1,
            tam: 1
        );
    }
}
