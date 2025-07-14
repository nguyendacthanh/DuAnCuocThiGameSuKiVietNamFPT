using System.Collections;
using UnityEngine;

public class classDonVi : MonoBehaviour
{
    // Thông số đơn vị
    private string TenDonVi, ChungQuan, TheLoai;
    private int SucCong, Mau, Giap, XungKich;
    private float TocDo, KhoiLuong;
    private int LuotDiChuyen, LuotTanCong;
    private float time = 0.3f;
    private int tamTanCong = 1; // mỗi tầm = 100 đơn vị

    private Coroutine coroutineDiChuyen;

    // Hàm khởi tạo thông tin đơn vị
    public virtual void Init(string ten, string loai, string quan, int cong, int hp, int giap, int xk,
                             float tocdo, float kl, int luotDi, int luotAtk, int tam)
    {
        TenDonVi = ten;
        TheLoai = loai;
        ChungQuan = quan;
        SucCong = cong;
        Mau = hp;
        Giap = giap;
        XungKich = xk;
        TocDo = tocdo;
        KhoiLuong = kl;
        LuotDiChuyen = luotDi;
        LuotTanCong = luotAtk;
        tamTanCong = tam;
    }

    // Di chuyển theo từng bước
    public virtual void DiChuyenDen(Vector3 viTriMoi)
    {
        if (LuotDiChuyen > 0)
        {
            if (coroutineDiChuyen != null)
                StopCoroutine(coroutineDiChuyen);

            coroutineDiChuyen = StartCoroutine(DiChuyenTuTu(transform.position, viTriMoi));
            LuotDiChuyen--;
        }
        else
        {
            Debug.Log($"{TenDonVi} không còn lượt di chuyển.");
        }
    }

    // Coroutine di chuyển
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

    // Tính dame xung kích
    public int TinhDameXungKich(Vector3 viTriBanDau, Vector3 viTriHienTai)
    {
        float dx = Mathf.Abs(viTriHienTai.x - viTriBanDau.x);
        float dy = Mathf.Abs(viTriHienTai.y - viTriBanDau.y);
        float soODiChuyen = (dx + dy) / 100f;

        float tongDame = soODiChuyen * (XungKich + KhoiLuong) + SucCong;
        if (tongDame < 10) tongDame = 10;

        return Mathf.RoundToInt(tongDame);
    }

    // Tấn công
    public virtual int TanCong(classDonVi mucTieu)
    {
        Vector3 viTriBanDau = transform.position; // Giả sử đứng yên
        int dameXK = TinhDameXungKich(viTriBanDau, transform.position);
        int tongDame = dameXK + SucCong;

        float giam = (float)mucTieu.Giap / (mucTieu.Giap + 100);
        float gayRa = tongDame * (1 - giam);
        int satThuong = Mathf.RoundToInt(gayRa);

        mucTieu.Mau -= satThuong;
        Debug.Log($"{TenDonVi} tấn công {mucTieu.TenDonVi}, gây {satThuong} sát thương. HP còn lại: {mucTieu.Mau}");

        mucTieu.KiemTraMau();

        if (mucTieu.Mau > 0)
        {
            mucTieu.PhanDon(this);
        }

        return satThuong;
    }

    // Phản đòn
    public virtual void PhanDon(classDonVi doiThu)
    {
        int dame = Mathf.RoundToInt(SucCong * 0.75f);
        doiThu.Mau -= dame;

        Debug.Log($"{TenDonVi} phản đòn {doiThu.TenDonVi}, gây {dame} sát thương. HP còn lại: {doiThu.Mau}");

        doiThu.KiemTraMau();
    }

    // Kiểm tra máu
    public void KiemTraMau()
    {
        if (Mau <= 0)
        {
            Debug.Log($"{TenDonVi} đã bị tiêu diệt!");
            Destroy(gameObject);
        }
    }
}
