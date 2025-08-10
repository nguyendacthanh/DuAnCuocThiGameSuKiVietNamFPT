using UnityEngine;

namespace script
{
    public class CameraManager : MonoBehaviour
    {
        public float moveSpeed = 2000f;
        public float zoomSpeed = 5f;
        public float minZoom = 5f;
        public float maxZoom = 20f;

        public Vector2 minPosition = new Vector2(0, 0);
        public Vector2 maxPosition = new Vector2(2000, 2000);

        private Camera _cam;

        private void Start()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            DiChuyen();
            Zoom();
            GioiHanViTri();
        }

        private void DiChuyen()
        {
            float moveX = 0f;
            float moveY = 0f;

            if (Input.GetKey(KeyCode.W)) moveY += 1f;
            if (Input.GetKey(KeyCode.S)) moveY -= 1f;
            if (Input.GetKey(KeyCode.A)) moveX -= 1f;
            if (Input.GetKey(KeyCode.D)) moveX += 1f;

            Vector3 move = new Vector3(moveX, moveY, 0f).normalized * (Time.deltaTime * moveSpeed);
            transform.position += move;
        }

        private void Zoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f && _cam.orthographic)
            {
                _cam.orthographicSize -= scroll * zoomSpeed;
                _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, minZoom, maxZoom);
            }
        }

        private void GioiHanViTri()
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
            pos.y = Mathf.Clamp(pos.y, minPosition.y, maxPosition.y);
            transform.position = pos;
        }
    }
}