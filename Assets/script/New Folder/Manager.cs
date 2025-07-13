using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gridChonPrefab;
    public GameObject gridDiChuyenPrefab;
    public GameObject gridAttackPrefab;

    private XuLyClick xuLyClick;

    void Start()
    {
        float khoangCachChon = 100f;
        float clickToiGrid = 50f;
        float banKinhTanCong = 150f;

        ChonDonVi chonDonVi = new ChonDonVi(khoangCachChon);
        Grid grid = new Grid(gridChonPrefab, gridDiChuyenPrefab, gridAttackPrefab);

        xuLyClick = new XuLyClick(
            chonDonVi, grid,
            khoangCachChon, clickToiGrid, banKinhTanCong
        );
    }

    void Update()
    {
        xuLyClick.XuLyClickNguoiChoi(); // gọi hàm xử lý click mỗi frame
    }
}
