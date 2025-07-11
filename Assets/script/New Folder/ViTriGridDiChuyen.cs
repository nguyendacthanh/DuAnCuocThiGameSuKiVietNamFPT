using System.Collections.Generic;
using UnityEngine;

public class ViTriGridDiChuyen
{
    private List<Vector3> danhSachViTri = new List<Vector3>();

    public void ThemViTri(Vector3 viTri)
    {
        danhSachViTri.Add(viTri);
    }

    public void XoaTatCa()
    {
        danhSachViTri.Clear();
    }

    public List<Vector3> LayTatCa()
    {
        return new List<Vector3>(danhSachViTri); // Trả bản sao an toàn
    }

    public int DemSoLuong()
    {
        return danhSachViTri.Count;
    }
}
