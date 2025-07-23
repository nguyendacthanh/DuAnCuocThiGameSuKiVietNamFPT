using Unity.VisualScripting;
using UnityEngine;

public class SatThuong : MonoBehaviour
{
    private int sucCong, xungKich, khoiLuong;
    private int tongDame;

    // Nhận dữ liệu từ đơn vị tấn công
    public void KhoiTao(classDonVi nguon)
    {
        sucCong = nguon.SucCong;
        xungKich = nguon.XungKich;
        khoiLuong = nguon.KhoiLuong;
        int soODiChuyen = nguon.soOdiChuyen;

        // Tính sát thương xung kích
        tongDame = soODiChuyen * (xungKich + khoiLuong) + sucCong;
        if (tongDame < 10) tongDame = 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Army"))
        {
            classDonVi mucTieu = other.GetComponent<classDonVi>();
            if (mucTieu != null)
            {
                // Tính sát thương thực tế (có tính giáp)
                int satThuongThuc = Mathf.RoundToInt(tongDame * (1 - mucTieu.Giap / (float)(mucTieu.Giap + 100)));

                mucTieu.Mau -= satThuongThuc;

                Debug.Log($"Gây {satThuongThuc} sát thương cho {mucTieu.name}");

                if (mucTieu.Mau <= 0)
                {
                    Destroy(mucTieu.gameObject);
                }

                Destroy(gameObject); // Xoá hiệu ứng sát thương sau khi chạm
            }
        }
    }
    
}