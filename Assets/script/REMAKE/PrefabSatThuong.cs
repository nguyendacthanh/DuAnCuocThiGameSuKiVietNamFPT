using UnityEngine;

public class PrefabSatThuong : MonoBehaviour
{
    private ClassDonVi nguon;

    public void KhoiTao(ClassDonVi nguon)
    {
        this.nguon = nguon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            ClassDonVi mucTieu = other.GetComponent<ClassDonVi>();
            if (mucTieu != null && nguon != null)
            {
                int satThuongThuc = nguon.TinhSatThuong(mucTieu);
                mucTieu.Hp -= satThuongThuc;
                if (mucTieu.Hp <= 0)
                {
                    Destroy(mucTieu.gameObject);
                }
                Destroy(gameObject,1f);
            }
        }
    }
}
