using UnityEngine;

public class classDonVi : MonoBehaviour
{
    public string TenDonVi, ChungQuan, TheLoai;
    public int SucCong, Mau, Giap, XungKich;
    public float TocDo, KhoiLuong;
    public int LuotDiChuyen, LuotTanCong;

    public virtual void TanCong(classDonVi mucTieu) { /* logic chung */ }
    public virtual void PhanDon(classDonVi doiThu) { /* logic phản đòn */ }
    public virtual void DiChuyen(Vector2 huong) { /* di chuyển */ }
}
