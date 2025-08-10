using UnityEngine;

public class MapLimit : MonoBehaviour
{
    public int MaxX = 900;
    public int MaxY = 900;
    public static MapLimit Instance;

    private void Awake()
    {
        Instance = this;
    }
}
