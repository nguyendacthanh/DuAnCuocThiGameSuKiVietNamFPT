using System.Collections.Generic;

using UnityEngine;

public class actions
{
    //lay toa do click
    // private GameObject ButtonInformation;
    public static Vector2 ToaDoClick(Vector2 clickPos)
    {
        float x = Mathf.Round(clickPos.x / 100f) * 100f;
        float y = Mathf.Round(clickPos.y / 100f) * 100f;
        return new Vector2(x, y);
    }

    //hien thi grid
    public void HienThiGridDiChuyen(Vector2 viTri, GameObject prefab)
    {
        Object.Instantiate(prefab, viTri, Quaternion.identity);
    }

    public void HienThiGridChon(Vector2 viTri, GameObject prefab)
    {
        Object.Instantiate(prefab, viTri, Quaternion.identity);
    }

    public void HienThiGridAttack(Vector2 viTri, GameObject prefab)
    {
        Object.Instantiate(prefab, viTri, Quaternion.identity);
    }

    public void HienThiGridEnemy(Vector2 viTri, GameObject prefab)
    {
        Object.Instantiate(prefab, viTri, Quaternion.identity);
    }

    //tam di chuyen cua player
    public static List<Vector2> TamDiChuyen(Vector2 gameObjectPosition, int banKinh)
    {
        List<Vector2> ketQua = new List<Vector2>();

        for (int dx = -banKinh; dx <= banKinh; dx++)
        {
            for (int dy = -banKinh; dy <= banKinh; dy++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= banKinh)
                {
                    Vector2 diem = gameObjectPosition + new Vector2(dx * 100, dy * 100);
                    ketQua.Add(diem);
                }
            }
        }

        return ketQua;
    }

    //
    private List<GameObject> Player = new List<GameObject>();
    private List<GameObject> Enemy = new List<GameObject>();

    // cập nhật lại danh sách của army và enemy mỗi khi có hành động xảy ra
    public void CapNhatToaDo()
    {
        Player.Clear();
        Enemy.Clear();
        Player.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        Enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private GameObject TimTheoViTri(Vector2 ClickPossition, List<GameObject> list)
    {
        foreach (var obj in list)
        {
            if ((Vector2)obj.transform.position == ClickPossition)
            {
                return obj;
            }
        }

        return null;
    }

    public GameObject TimEnemy(Vector2 clickPosition)
    {
        return TimTheoViTri(clickPosition, Enemy);
    }

    public GameObject TimPlayer(Vector2 clickPosition)
    {
        return TimTheoViTri(clickPosition, Player);
    }

    public List<GameObject> TimEnemyTrongTamDanh(GameObject player, int RangeAtk)
    {
        List<GameObject> ketQua = new List<GameObject>();

        if (player == null) return ketQua;

        Vector2 viTriGoc = player.transform.position;

        for (int dx = -RangeAtk; dx <= RangeAtk; dx++)
        {
            for (int dy = -RangeAtk; dy <= RangeAtk; dy++)
            {
                // Hình thoi: chỉ lấy các điểm có khoảng cách Manhattan <= tocDo
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= RangeAtk)
                {
                    Vector2 viTriCanKiemTra = viTriGoc + new Vector2(dx * 100, dy * 100);

                    GameObject enemy = TimEnemy(viTriCanKiemTra);
                    if(enemy != null && !ketQua.Contains(enemy))
                    {
                        ketQua.Add(enemy);
                    }
                }
            }
        }

        return ketQua;
    }

    public void ClickEvent( GameObject prefabGridEnemy,
        GameObject prefabGridDiChuyen, GameObject prefabGridAttack, GameObject prefabGridChon,
        GameObject ButtonInformation)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 toaDoClick = ToaDoClick(clickWorld); // Tọa độ lưới
            CapNhatToaDo(); // Cập nhật danh sách đơn vị
            GameObject enemyObj = TimEnemy(toaDoClick);
            GameObject armyObj = TimPlayer(toaDoClick);
            if (DiChuyen(toaDoClick)) return;
            if (TanCong(toaDoClick))
            {
                CapNhatButtonInformation(ButtonInformation);
                return;
            }

            if (ClickEnemy(toaDoClick, enemyObj, prefabGridEnemy))
            {
                CapNhatButtonInformation(ButtonInformation);
                return;
            }

            if (ClickPlayer(toaDoClick, armyObj, prefabGridAttack, prefabGridChon, prefabGridDiChuyen))
            {
                CapNhatButtonInformation(ButtonInformation);
                return;
            }

            ClickTerrain(toaDoClick, prefabGridChon);
            CapNhatButtonInformation(ButtonInformation);
        }
    }

    //=======================Hành động của player khi click=====================//
    private bool DiChuyen(Vector2 clickPos)
    {
        if (!CoGridMoveTaiViTri(clickPos)) return false;

        foreach (GameObject player in Player)
        {
            var donVi = player.GetComponent<ClassDonVi>();
            if (donVi.isSelected && donVi.CurrentSpeed > 0)
            {
                donVi.Move(clickPos);
                XoaGridTheoTag("GridMove");
                XoaGridTheoTag("GridAttack");
                return true;
            }
        }

        return false;
    }

    private bool TanCong(Vector2 clickPos)
    {
        if (!CoGridAttackTaiViTri(clickPos)) return false;

        GameObject enemy = TimEnemy(clickPos);
        if (enemy == null) return false;

        foreach (GameObject army in Player)
        {
            var donVi = army.GetComponent<ClassDonVi>();
            if (donVi.isSelected && donVi.CurrentAtk > 0)
            {
                donVi.Attack(enemy,army);
                XoaGridTheoTag("GridAttack");
                XoaGridTheoTag("GridMove");
                return true;
            }
        }

        return false;
    }

    private Vector2? viTriGridEnemy = null, viTriGridChon = null, viTriGridDiChuyen = null;


    //sự kiện click enemy
    private bool ClickEnemy(Vector2 clickPos, GameObject enemyObj, GameObject prefabGridEnemy)
    {
        if (enemyObj == null) return false;
        var donViEnemy = enemyObj.GetComponent<ClassDonVi>();
        // var donViPlayer = armyObj.GetComponent<ClassDonVi>();


        if (viTriGridEnemy != null && viTriGridEnemy == clickPos)
        {
            donViEnemy.isSelected = false;
            XoaGridTheoTag("GridEnemy");
            viTriGridEnemy = null;
        }
        else
        {
            donViEnemy.isSelected = true;
            // donViPlayer.isSelected = false;
            enemyObj.GetComponent<ClassDonVi>().isSelected = true;
            XoaGridTheoTag("GridEnemy");
            HienThiGridEnemy(clickPos, prefabGridEnemy);
            viTriGridEnemy = clickPos;

            XoaGridTheoTag("Grid");
            XoaGridTheoTag("GridMove");
            XoaGridTheoTag("GridAttack");
            viTriGridChon = null;
            viTriGridDiChuyen = null;
        }

        foreach (GameObject army in Player)
            army.GetComponent<ClassDonVi>().isSelected = false;

        return true;
    }

    //sự kiện click Player
    private bool ClickPlayer(Vector2 clickPos, GameObject armyObj, GameObject prefabGridAttack,
        GameObject prefabGridChon, GameObject prefabGridDiChuyen)
    {
        if (armyObj == null) return false;

        var donViPlayer = armyObj.GetComponent<ClassDonVi>();
        //var donViEnemy = enemyObj.GetComponent<ClassDonVi>();


        if (donViPlayer.isSelected)
        {
            donViPlayer.isSelected = false;
            XoaGridTheoTag("GridMove");
            XoaGridTheoTag("Grid");
            XoaGridTheoTag("GridAttack");
            viTriGridDiChuyen = null;
        }
        else
        {
            // donViEnemy.isSelected = false;
            foreach (GameObject army in Player)
                army.GetComponent<ClassDonVi>().isSelected = false;

            donViPlayer.isSelected = true;

            XoaGridTheoTag("GridAttack");
            if (donViPlayer.CurrentAtk > 0)
            {
                var enemies = TimEnemyTrongTamDanh(donViPlayer.gameObject, donViPlayer.RangeAtk);
                foreach (var enemy in enemies)
                {
                    HienThiGridAttack(enemy.transform.position, prefabGridAttack);
                }
            }

            XoaGridTheoTag("GridMove");
            XoaGridTheoTag("Grid");

            HienThiGridChon(clickPos, prefabGridChon);
            if (donViPlayer.CurrentSpeed > 0)
            {
                var posList = TamDiChuyen(donViPlayer.transform.position, donViPlayer.Speed);
                foreach (Vector2 pos in posList)
                {
                    if (TimPlayer(pos) == null && TimEnemy(pos) == null)
                    {
                        HienThiGridDiChuyen(pos, prefabGridDiChuyen);
                    }
                }

                viTriGridDiChuyen = donViPlayer.transform.position;
            }

            XoaGridTheoTag("Grid");
            XoaGridTheoTag("GridEnemy");

            HienThiGridChon(clickPos, prefabGridChon);
            viTriGridChon = null;
            viTriGridEnemy = null;
        }

        return true;
    }

    private void ClickTerrain(Vector2 clickPos, GameObject prefabGridChon)
    {
        if (viTriGridChon != null && viTriGridChon == clickPos)
        {
            XoaGridTheoTag("Grid");
            viTriGridChon = null;
        }
        else
        {
            XoaGridTheoTag("Grid");
            HienThiGridChon(clickPos, prefabGridChon);
            viTriGridChon = clickPos;
        }

        XoaGridTheoTag("GridEnemy");
        XoaGridTheoTag("GridMove");
        XoaGridTheoTag("GridAttack");
        viTriGridEnemy = null;
        viTriGridDiChuyen = null;

        foreach (GameObject army in Player)
            army.GetComponent<ClassDonVi>().isSelected = false;
        foreach (GameObject army in Enemy)
            army.GetComponent<ClassDonVi>().isSelected = false;
    }


    //xóa grid theo tag
    static void XoaGridTheoTag(string tag)
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject g in grids)
        {
            GameObject.Destroy(g);
        }
    }

     public static void XoaTatCaGridVaUi()
    {
        XoaGridTheoTag("GridEnemy");
        XoaGridTheoTag("GridMove");
        XoaGridTheoTag("GridAttack");
        XoaGridTheoTag("Grid");
        GameObject[] army = GameObject.FindGameObjectsWithTag("Player");
        foreach (var a in army)
        {
            a.GetComponent<ClassDonVi>().isSelected = false;
        }
    }

    //Kiểm tra xem tại vị trí click có Grid ko
    private bool CoGridMoveTaiViTri(Vector2 viTri)
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(viTri);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("GridMove"))
                return true;
        }

        return false;
    }

    private bool CoGridAttackTaiViTri(Vector2 viTri)
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(viTri);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("GridAttack"))
                return true;
        }

        return false;
    }


    private void CapNhatButtonInformation(GameObject buttonInformation)
    {
        foreach (GameObject player in Player)
        {
            if (player.GetComponent<ClassDonVi>().isSelected)
            {
                buttonInformation.SetActive(true);
                return;
            }
        }

        foreach (GameObject enemy in Enemy)
        {
            if (enemy.GetComponent<ClassDonVi>().isSelected)
            {
                buttonInformation.SetActive(true);
                return;
            }
        }

        buttonInformation.SetActive(false);
    }

    // private void PassiveSkillList()
    // {
    //     List<PassiveSkill> passiveSkills = new List<PassiveSkill>();
    // }
    
}