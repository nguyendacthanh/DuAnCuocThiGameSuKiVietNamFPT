using System.Collections;
using UnityEngine;

public class classDonVi : MonoBehaviour
{
    public string TenDonVi, ChungQuan, TheLoai;
    public int SucCong, Mau, Giap, XungKich;
    public float TocDo, KhoiLuong;
    public int LuotDiChuyen, LuotTanCong;
    public float time = 0.3f;
    public int tamTanCong = 1; // mỗi "1" là 100 đơn vị
    public float KhoangCachTimKeThu = 300f;

    private Coroutine coroutineDiChuyen;
    private Vector3 viTriBanDau;

    void Start()
    {
        viTriBanDau = transform.position;
    }

    public virtual void DiChuyenDen(Vector3 viTriMoi)
    {
        if (LuotDiChuyen > 0)
        {
            if (coroutineDiChuyen != null)
                StopCoroutine(coroutineDiChuyen);

            viTriBanDau = transform.position;
            coroutineDiChuyen = StartCoroutine(DiChuyenTuTu(viTriBanDau, viTriMoi));
            LuotDiChuyen--;
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

        KiemTraVaTanCongKeThu();
    }

    public int TinhDameXungKich()
    {
        Vector3 posNow = transform.position;
        float dx = Mathf.Abs(posNow.x - viTriBanDau.x);
        float dy = Mathf.Abs(posNow.y - viTriBanDau.y);
        float soODiChuyen = (dx + dy) / 100f;

        float tong = soODiChuyen * (XungKich + KhoiLuong) + SucCong;
        if (tong < 10) tong = 10;

        return Mathf.RoundToInt(tong);
    }

    public virtual int TanCong(classDonVi mucTieu)
    {
        int dameXK = TinhDameXungKich();
        int tongDame = dameXK + SucCong;

        float giam = (float)mucTieu.Giap / (mucTieu.Giap + 100);
        float gayRa = tongDame * (1 - giam);

        int satThuong = Mathf.RoundToInt(gayRa);
        mucTieu.Mau -= satThuong;

        Debug.Log($"{TenDonVi} tấn công {mucTieu.TenDonVi}, gây {satThuong} sát thương. HP còn lại: {mucTieu.Mau}");

        mucTieu.KiemTraMau();
        if (mucTieu.Mau > 0) mucTieu.PhanDon(this);

        return satThuong;
    }

    public virtual void PhanDon(classDonVi doiThu)
    {
        int dame = Mathf.RoundToInt(SucCong * 0.75f);
        doiThu.Mau -= dame;

        Debug.Log($"{TenDonVi} phản đòn {doiThu.TenDonVi}, gây {dame} sát thương. HP còn lại: {doiThu.Mau}");

        doiThu.KiemTraMau();
    }

    public void KiemTraMau()
    {
        if (Mau <= 0)
        {
            Debug.Log($"{TenDonVi} đã bị tiêu diệt!");
            Destroy(gameObject);
        }
    }

    public void KiemTraVaTanCongKeThu()
    {
        if (LuotTanCong <= 0) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = float.MaxValue;
        classDonVi enemyGanNhat = null;

        foreach (GameObject go in enemies)
        {
            float dist = Vector3.Distance(go.transform.position, transform.position);
            if (dist <= KhoangCachTimKeThu && dist < minDist)
            {
                enemyGanNhat = go.GetComponent<classDonVi>();
                minDist = dist;
            }
        }

        if (enemyGanNhat != null && minDist <= tamTanCong * 100)
        {
            TanCong(enemyGanNhat);
            LuotTanCong--;
        }
    }
}
