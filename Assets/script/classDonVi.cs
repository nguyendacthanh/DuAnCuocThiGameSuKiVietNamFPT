using System.Collections;
using UnityEngine;

public class classDonVi : MonoBehaviour
{
    public bool isSelected = false;
        public string TenDonVi, ChungQuan, TheLoai;
    public int SucCong, Mau, Giap, XungKich;
    public int TocDo, KhoiLuong;
    public float time = 0.3f;
    public int tamTanCong = 1;

    private Coroutine coroutineDiChuyen;

    public int maxLuotDiChuyen;
    public int maxLuotTanCong;
    public int LuotDiChuyen;
    public int LuotTanCong;
    public int soOdiChuyen=0;

    // Khởi tạo đơn vị
    public virtual void Init(string ten, string loai, string quan, int cong, int hp, int giap, int xk,
                             int tocdo, int kl, int luotDi, int luotAtk, int tam)
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
        maxLuotDiChuyen = luotDi;
        maxLuotTanCong = luotAtk;
        LuotDiChuyen = maxLuotDiChuyen;
        LuotTanCong = maxLuotTanCong;
        tamTanCong = tam;
        Debug.Log($"[INIT] {ten} | LuotDi: {LuotDiChuyen}, LuotTanCong: {LuotTanCong}");
    }

    public virtual void DiChuyenDen(Vector3 viTriMoi)
    {
        if (LuotDiChuyen > 0 )
        {
            if (coroutineDiChuyen != null)
                StopCoroutine(coroutineDiChuyen);

            Vector3 startSnap = new Vector3(
                Mathf.Round(transform.position.x / 100f) * 100f,
                Mathf.Round(transform.position.y / 100f) * 100f,
                transform.position.z
            );
            coroutineDiChuyen = StartCoroutine(MoveRule(startSnap, viTriMoi));
            LuotDiChuyen--;
        }
        else
        {
            Debug.Log($"{TenDonVi} không còn lượt di chuyển.");
        }
    }

    private IEnumerator MoveRule(Vector3 start, Vector3 target)
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
            soOdiChuyen++;
        }

        for (int i = 0; i < Mathf.Abs(buocY); i++)
        {
            currentPos += new Vector3(0, stepY * 100f, 0);
            transform.position = currentPos;
            yield return new WaitForSeconds(time);
            soOdiChuyen++;
        }
    }

    public int TinhDameXungKich(int soOdiChuyen)
    {
        float tongDame = soOdiChuyen * (XungKich + KhoiLuong) + SucCong;
        if (tongDame < 10) tongDame = 10;

        return Mathf.RoundToInt(tongDame);
    } 

    public virtual void TanCong()
    {
        if (LuotTanCong > 0)
        {
            LuotTanCong--;
        }
    }
    //
    // public virtual void PhanDon(classDonVi doiThu)
    // {
    //     int dame = Mathf.RoundToInt(SucCong * 0.75f);
    //     doiThu.Mau -= dame;
    //     Debug.Log($"{TenDonVi} phản đòn {doiThu.TenDonVi}, gây {dame} sát thương. HP còn lại: {doiThu.Mau}");
    //     doiThu.KiemTraMau();
    // }
    //
    public void KiemTraMau()
    {
        if (Mau <= 0)
        {
            Debug.Log($"{TenDonVi} đã bị tiêu diệt!");
            Destroy(gameObject);
        }
    }

    public void ResetLuot()
    {
        LuotDiChuyen = 0;
        LuotTanCong = 0;
    }

    public void KhoiPhucLuot()
    {
        LuotDiChuyen = maxLuotDiChuyen;
        LuotTanCong = maxLuotTanCong;
        Debug.Log($"{TenDonVi} được khôi phục lượt: {LuotDiChuyen} đi, {LuotTanCong} đánh");
    }
}
