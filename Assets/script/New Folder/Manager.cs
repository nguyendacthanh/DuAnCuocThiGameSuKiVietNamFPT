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

        ChonDonVi chonDonVi = new ChonDonVi(khoangCachChon);
        Grid grid = new Grid(gridChonPrefab, gridDiChuyenPrefab, gridAttackPrefab);

        xuLyClick = new XuLyClick(chonDonVi, grid, khoangCachChon, clickToiGrid);
    }

    void Update()
    {
        xuLyClick.XuLyClickNguoiChoi();
    }
}
