// using UnityEngine;
//
//
// public class PrefabSatThuong : MonoBehaviour
// {
//     public int dame;
//     public ClassDonVi nguon; // đơn vị nguồn gây sát thương
//     public GameObject prefabTextMesh;
//     
//     
//     // Lấy dame từ class đơn vị và đơn vị đã tấn công
//     public void Dame(ClassDonVi _nguon)
//     {
//         nguon = _nguon;
//         dame = nguon.TotalDame();
//     }
//
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player") || other.CompareTag("Enemy"))
//         {
//             var mucTieu = other.GetComponent<ClassDonVi>();
//             if (mucTieu != null)
//             {
//                 mucTieu.NhanSatThuong(dame, nguon);
//                 if (prefabTextMesh != null)
//                 {
//                     
//                     GameObject go = Instantiate(prefabTextMesh, other.transform.position, Quaternion.identity);
//                     var textScript = go.GetComponent<UIHienThiSatThuong>();
//                     if (textScript != null)
//                     {
//                         textScript.KhoiTao(dame);
//                     }
//                 }
//                 Destroy(gameObject);
//             }
//         }
//     }
// }
//
