using UnityEngine;
using System.Collections.Generic;
public class XuLyClick
{
    private ChonDonVi unitSelector;
    private Grid gridHandler;
    private float khoangCachGioiHan, khoangCachClickToiGrid;

    private GameObject donViDangChon = null;
    private bool gridActive = false;

    public XuLyClick(ChonDonVi selector, Grid grid, float khoangCachGioiHan, float khoangCachClickToiGrid)
    {
        this.unitSelector = selector;
        this.gridHandler = grid;
        this.khoangCachGioiHan = khoangCachGioiHan;
        this.khoangCachClickToiGrid = khoangCachClickToiGrid;
    }

    public void XuLyClickNguoiChoi()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clickPos = new Vector2(mousePos.x, mousePos.y);

        GameObject donViDuocChon = unitSelector.TimDonViGanNhat(clickPos);

        if (donViDangChon != null && gridActive)
        {
            List<Vector3> viTriGrid = gridHandler.LayDanhSachViTriGridDiChuyen();
            foreach (Vector3 viTri in viTriGrid)
            {
                if (Vector2.Distance(clickPos, viTri) <= khoangCachClickToiGrid)
                {
                    classDonVi scriptDonVi = donViDangChon.GetComponent<classDonVi>();
                    scriptDonVi.DiChuyenDen(viTri);
                    gridHandler.XoaTatCaGrid();
                    gridActive = false;

                    if (scriptDonVi.LuotDiChuyen <= 0 && scriptDonVi.LuotTanCong > 0)
                    {
                        gridHandler.HienThiGridAttack(donViDangChon.transform.position, scriptDonVi.tamTanCong);
                    }

                    return;
                }
            }
        }

        if (donViDuocChon == null)
        {
            gridHandler.XoaTatCaGrid();
            gridActive = false;
            donViDangChon = null;
            return;
        }

        if (donViDangChon == null || donViDuocChon != donViDangChon)
        {
            donViDangChon = donViDuocChon;
            gridActive = true;

            gridHandler.XoaTatCaGrid();
            classDonVi script = donViDangChon.GetComponent<classDonVi>();

            if (script.LuotDiChuyen <= 0)
            {
                gridHandler.HienThiGridChon(donViDangChon.transform.position);
                if (script.LuotTanCong > 0)
                    gridHandler.HienThiGridAttack(donViDangChon.transform.position, script.tamTanCong);
            }
            else
            {
                gridHandler.TaoOGridHinhThoi(donViDangChon.transform.position, Mathf.RoundToInt(script.TocDo));
                if (script.LuotTanCong > 0)
                    gridHandler.HienThiGridAttack(donViDangChon.transform.position, script.tamTanCong);
            }
        }
        else
        {
            if (gridActive)
            {
                gridHandler.XoaTatCaGrid();
                gridActive = false;
            }
            else
            {
                gridActive = true;
                classDonVi script = donViDuocChon.GetComponent<classDonVi>();

                if (script.LuotDiChuyen <= 0)
                {
                    gridHandler.HienThiGridChon(donViDuocChon.transform.position);
                    if (script.LuotTanCong > 0)
                        gridHandler.HienThiGridAttack(donViDuocChon.transform.position, script.tamTanCong);
                }
                else
                {
                    gridHandler.TaoOGridHinhThoi(donViDuocChon.transform.position, Mathf.RoundToInt(script.TocDo));
                    if (script.LuotTanCong > 0)
                        gridHandler.HienThiGridAttack(donViDuocChon.transform.position, script.tamTanCong);
                }
            }
        }
    }
}
