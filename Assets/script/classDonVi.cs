using System.Collections;
using UnityEngine;

public class classDonVi : MonoBehaviour
{
    public string TenDonVi, ChungQuan, TheLoai;
    public int SucCong, Mau, Giap, XungKich;
    public float TocDo, KhoiLuong;
    public int LuotDiChuyen, LuotTanCong;
    public float time = 0.3f;
    public int tamTanCong = 1;
    private Coroutine coroutineDiChuyen;

    private Vector3 viTriBanDau; // lưu vị trí ban đầu

    void Start()
    {
        viTriBanDau = transform.position;
    }

    public virtual int TanCong(classDonVi mucTieu)
    {
        int dameXK = TinhDameXungKich(); 
        int tongDame = dameXK + SucCong;

        int giapMucTieu = mucTieu.Giap;
        float DameGiamDuoc = (float)giapMucTieu / (giapMucTieu + 100);
        float SatThuongGayRa = tongDame * (1 - DameGiamDuoc);
        int satThuong = Mathf.RoundToInt(SatThuongGayRa);

        Debug.Log($"{TenDonVi} tấn công {mucTieu.TenDonVi} gây {satThuong} sát thương!");

        mucTieu.TruMau(satThuong);

        return satThuong;
    }


    public virtual void PhanDon(classDonVi doiThu)
    {
        int damePhanDon = Mathf.RoundToInt(SucCong * 0.75f);

        Debug.Log($"{TenDonVi} phản đòn {doiThu.TenDonVi}, gây {damePhanDon} sát thương!");

        doiThu.TruMau(damePhanDon);
    }
    public virtual void DiChuyenDen(Vector3 viTriMoi)
    {
        if (LuotDiChuyen > 0)
        {
            if (coroutineDiChuyen != null)
                StopCoroutine(coroutineDiChuyen);

            viTriBanDau = transform.position; // cập nhật vị trí ban đầu trước khi di chuyển
            coroutineDiChuyen = StartCoroutine(DiChuyenTuTu(viTriBanDau, viTriMoi));

            LuotDiChuyen--;
            Debug.Log(TenDonVi + " bắt đầu di chuyển đến: " + viTriMoi + " | Còn " + LuotDiChuyen + " lượt.");
        }
        else
        {
            Debug.Log(TenDonVi + " không còn lượt di chuyển.");
        }
    }

    private IEnumerator DiChuyenTuTu(Vector3 start, Vector3 target)
    {
        Vector3 currentPos = start;

        int buocX = Mathf.RoundToInt((target.x - start.x) / 100f);
        int buocY = Mathf.RoundToInt((target.y - start.y) / 100f);
        int stepX = (int)Mathf.Sign(buocX);
        int stepY = (int)Mathf.Sign(buocY);

        for (int i = 0; i < Mathf.Abs(buocX); i++)
        {
            currentPos += new Vector3(stepX * 100f, 0, 0);
            transform.position = currentPos;
            yield return new WaitForSeconds(time);
        }

        for (int i = 0; i < Mathf.Abs(buocY); i++)
        {
            currentPos += new Vector3(0, stepY * 100f, 0);
            transform.position = currentPos;
            yield return new WaitForSeconds(time);
        }
    }

    // ✅ Hàm tính dame xung kích
    public int TinhDameXungKich()
    {
        Vector3 viTriHienTai = transform.position;
        float dx = Mathf.Abs(viTriHienTai.x - viTriBanDau.x);
        float dy = Mathf.Abs(viTriHienTai.y - viTriBanDau.y);

        float soODiChuyen = (dx + dy) / 100f;
        float tongDame = soODiChuyen * (XungKich+KhoiLuong) + SucCong;
        if (tongDame <= 10)
        {
            tongDame = 10;
        }

        return Mathf.RoundToInt(tongDame);
    }
    public void TruMau(int satThuong)
    {
        Mau -= satThuong;

        Debug.Log($"{TenDonVi} bị trừ {satThuong} máu! Còn lại: {Mau}");

        if (Mau <= 0)
        {
            Debug.Log($"{TenDonVi} đã bị tiêu diệt!");
            GameObject.Destroy(this.gameObject);
        }
    }
}
