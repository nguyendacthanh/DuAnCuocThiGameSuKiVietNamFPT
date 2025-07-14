using System.Collections;
using UnityEngine;

public class classDonVi : MonoBehaviour
{
    public string TenDonVi, ChungQuan, TheLoai;
    public int SucCong, Mau, Giap, XungKich;
    public float TocDo, KhoiLuong;
    public int LuotDiChuyen, LuotTanCong;
    public float time = 0.3f;
    public int tamTanCong = 1; // Tầm tấn công, đơn vị: 1 = 100, 2 = 200,...

    private Coroutine coroutineDiChuyen;

    public virtual void TanCong(classDonVi mucTieu) { }

    public virtual void PhanDon(classDonVi doiThu) { }

    public virtual void DiChuyenDen(Vector3 viTriMoi)
    {
        if (LuotDiChuyen > 0)
        {
            if (coroutineDiChuyen != null)
                StopCoroutine(coroutineDiChuyen);

            coroutineDiChuyen = StartCoroutine(DiChuyenTuTu(transform.position, viTriMoi));
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
}
