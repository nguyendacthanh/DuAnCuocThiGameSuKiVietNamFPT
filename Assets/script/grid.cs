using UnityEngine;

public class grid : MonoBehaviour
{
        public GameObject tilePrefab; // Prefab hình vuông (PPU = 100)
        public int gridWidth = 100;
        public int gridHeight = 100;
        public float tileSize = 1f; // 1 đơn vị = 100px nếu PPU = 100
    
        void Start()
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    Vector3 pos = new Vector3(x * tileSize, y * tileSize, 0);
                    Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                }
            }
        }
}
