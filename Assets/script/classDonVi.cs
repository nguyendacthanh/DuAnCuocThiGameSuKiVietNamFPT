using UnityEngine;

public class classDonVi : MonoBehaviour
{
    public string TenDonVi, ChungQuan, TheLoai;
    public int SucCong, Mau, Giap, XungKich;
    public float TocDo, KhoiLuong;
    public int LuotDiChuyen, LuotTanCong;

    public virtual void TanCong(classDonVi mucTieu) { /* logic chung */ }
    public virtual void PhanDon(classDonVi doiThu) { /* logic phản đòn */ }
    public virtual void DiChuyenDen(Vector3 viTriMoi)
    {
        if (LuotDiChuyen > 0)
        {
            transform.position = viTriMoi;
            LuotDiChuyen--;
            Debug.Log(TenDonVi + " di chuyển đến: " + viTriMoi + " | Còn " + LuotDiChuyen + " lượt.");
        }
        else
        {
            Debug.Log(TenDonVi + " không còn lượt di chuyển.");
        }
    }
}
