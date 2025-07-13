using System.Collections.Generic;
using UnityEngine;

public class QuanLySuKienChonDonVi : MonoBehaviour
{
    public float KhoangCachGioiHanXY = 50f;
    public float KhoangCachClickToiGrid = 30f;
    public GameObject GridDiChuyen, GridChon, GridAttack;
    public float BanKinhTanCong = 101f;

    private XuLyClick xuLyClick;

    void Start()
    {
        var selector = new ChonDonVi(KhoangCachGioiHanXY);
        var grid = new Grid(GridChon, GridDiChuyen);

        xuLyClick = new XuLyClick(selector, grid, GridChon, GridDiChuyen, GridAttack,
            KhoangCachGioiHanXY, KhoangCachClickToiGrid, BanKinhTanCong);
    }

    void Update()
    {
        xuLyClick.XuLyClickNguoiChoi(); // 👈 Gọi xử lý click đã tách riêng
    }
}
