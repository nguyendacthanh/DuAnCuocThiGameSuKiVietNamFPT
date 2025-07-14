using System.Collections;
using UnityEngine;

public class classDonVi : MonoBehaviour
{
    public string TenDonVi, ChungQuan, TheLoai;
    public int SucCong, Mau, Giap, XungKich;
    public float TocDo, KhoiLuong;
    public int LuotDiChuyen, LuotTanCong;
    public float time = 0.3f; // thời gian để thực hiện mỗi bước (di chuyển 100 đơn vị)

    private Coroutine coroutineDiChuyen;

    public virtual void TanCong(classDonVi mucTieu) { /* logic tấn công */ }

    public virtual void PhanDon(classDonVi doiThu) { /* logic phản đòn */ }

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

        // Tính số bước cần đi
        int buocX = Mathf.RoundToInt((target.x - start.x) / 100f);
        int buocY = Mathf.RoundToInt((target.y - start.y) / 100f);

        int absX = Mathf.Abs(buocX);
        int absY = Mathf.Abs(buocY);

        int stepX = (int)Mathf.Sign(buocX);
        int stepY = (int)Mathf.Sign(buocY);

        // Di chuyển theo từng bước: ưu tiên X, sau đó Y
        for (int i = 0; i < absX; i++)
        {
            Vector3 next = currentPos + new Vector3(stepX * 100f, 0, 0);
            yield return StartCoroutine(DiChuyen1Buoc(currentPos, next));
            currentPos = next;
        }

        for (int i = 0; i < absY; i++)
        {
            Vector3 next = currentPos + new Vector3(0, stepY * 100f, 0);
            yield return StartCoroutine(DiChuyen1Buoc(currentPos, next));
            currentPos = next;
        }
    }

    private IEnumerator DiChuyen1Buoc(Vector3 start, Vector3 end)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }
}
