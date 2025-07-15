using UnityEngine;

public class ManagerClickEvent : MonoBehaviour
{
        public GameObject prefabGridChon;
    public GameObject prefabGridEnemy;

    private Grid grid;
    private Vector2? viTriGridChon = null;
    private Vector2? viTriGridEnemy = null;

    void Start()
    {
        grid = new Grid();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 toaDoClick = click.ToaDoClick(clickWorld);

            bool trungEnemy = false;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                if ((Vector2)enemy.transform.position == toaDoClick)
                {
                    trungEnemy = true;
                    break;
                }
            }

            if (trungEnemy)
            {
                // Toggle GridEnemy
                if (viTriGridEnemy != null && viTriGridEnemy == toaDoClick)
                {
                    XoaGridTheoTag("GridEnemy");
                    viTriGridEnemy = null;
                }
                else
                {
                    XoaGridTheoTag("GridEnemy");
                    grid.HienThiGridEnemy(toaDoClick, prefabGridEnemy);
                    viTriGridEnemy = toaDoClick;

                    // Cũng nên xoá GridChon nếu có
                    XoaGridTheoTag("Grid");
                    viTriGridChon = null;
                }
            }
            else
            {
                // Toggle GridChon
                if (viTriGridChon != null && viTriGridChon == toaDoClick)
                {
                    XoaGridTheoTag("Grid");
                    viTriGridChon = null;
                }
                else
                {
                    XoaGridTheoTag("Grid");
                    grid.HienThiGridChon(toaDoClick, prefabGridChon);
                    viTriGridChon = toaDoClick;

                    // Cũng nên xoá GridEnemy nếu có
                    XoaGridTheoTag("GridEnemy");
                    viTriGridEnemy = null;
                }
            }
        }
    }

    private void XoaGridTheoTag(string tag)
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in grids)
        {
            Destroy(go);
        }
    }
}
