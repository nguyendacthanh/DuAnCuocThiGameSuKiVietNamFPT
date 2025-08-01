using UnityEngine;

public class PrefabSatThuong : MonoBehaviour
{
    private int sucCong, xungKich, khoiLuong;
    private int tongDame;

    // Nhận dữ liệu từ đơn vị tấn công
    public void KhoiTao(ClassDonVi nguon)
    {
        sucCong = nguon.Atk;
        xungKich = nguon.Charge;
        khoiLuong = nguon.Mass;
        int soODiChuyen = nguon.NumberBlock;

        // Tính sát thương xung kích
        tongDame = soODiChuyen * (xungKich + khoiLuong) + sucCong;
        if (tongDame < 10) tongDame = 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            ClassDonVi mucTieu = other.GetComponent<ClassDonVi>();
            if (mucTieu != null)
            {
                // Tính sát thương thực tế (có tính giáp)
                int satThuongThuc = Mathf.RoundToInt(tongDame * (1 - mucTieu.Def / (float)(mucTieu.Def + 100)));

                mucTieu.Hp -= satThuongThuc;

                Debug.Log($"Gây {satThuongThuc} sát thương cho {mucTieu.name}");

                if (mucTieu.Hp <= 0)
                {
                    Destroy(mucTieu.gameObject);
                }

                Destroy(gameObject); // Xoá hiệu ứng sát thương sau khi chạm
            }
        }
    }
}
