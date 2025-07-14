using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private classDonVi donVi;
    public float KhoangCachTimKeThu = 300f;

    void Start()
    {
        donVi = GetComponent<classDonVi>();
    }

    void Update()
    {
        if (donVi.LuotDiChuyen <= 0 && donVi.LuotTanCong <= 0) return;

        GameObject[] armies = GameObject.FindGameObjectsWithTag("Army");
        classDonVi armyGanNhat = null;
        float minDist = float.MaxValue;

        foreach (GameObject go in armies)
        {
            float dist = Vector3.Distance(go.transform.position, transform.position);
            if (dist <= KhoangCachTimKeThu && dist < minDist)
            {
                minDist = dist;
                armyGanNhat = go.GetComponent<classDonVi>();
            }
        }

        if (armyGanNhat == null) return;

        float distToArmy = Vector3.Distance(armyGanNhat.transform.position, transform.position);
        float tamTanCong = donVi.tamTanCong * 100f;

        if (distToArmy <= tamTanCong && donVi.LuotTanCong > 0)
        {
            donVi.TanCong(armyGanNhat);
            donVi.LuotTanCong--;
        }
        else if (donVi.LuotDiChuyen > 0)
        {
            Vector3 viTriMucTieu = armyGanNhat.transform.position;
            donVi.DiChuyenDen(viTriMucTieu); // tự động gọi coroutine trong classDonVi
        }
    }
}
