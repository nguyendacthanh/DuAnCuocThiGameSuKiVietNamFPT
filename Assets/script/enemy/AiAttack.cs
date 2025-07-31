    using System.Collections.Generic;
    using UnityEngine;

    public class AiAttack
    {
        public static void TanCong(GameObject enemy, GameObject prefabSatThuong)
        {
            ClassDonVi enemyDonVi = enemy.GetComponent<ClassDonVi>();
            if (enemyDonVi.CurrentAtk <= 0) return;

            GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
            List<GameObject> mucTieuTrongTam = new List<GameObject>();

            foreach (GameObject army in armies)
            {
                float dist = Vector3.Distance(enemy.transform.position, army.transform.position);
                if (dist <= enemyDonVi.RangeAtk * 100f)
                {
                    mucTieuTrongTam.Add(army);
                }
            }

            if (mucTieuTrongTam.Count == 0) return;

            GameObject mucTieu = mucTieuTrongTam[0];

            // Kiểm tra null trước khi Instantiate
            if (prefabSatThuong == null)
            {
                Debug.LogError("Prefab sát thương chưa được gán!");
                return;
            }

            GameObject hitEffect = Object.Instantiate(prefabSatThuong, mucTieu.transform.position, Quaternion.identity);
            SatThuong st = hitEffect.GetComponent<SatThuong>();
            st.KhoiTao(enemyDonVi);

            enemyDonVi.CurrentAtk = Mathf.Max(0, enemyDonVi.CurrentAtk - 1);
        }
    }
