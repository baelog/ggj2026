using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float edgeSize = 0.10f;
    public float edgeSizeMax = 0.05f;
    public BoxCollider2D worldLimit;

    Camera cam;
    private float camHalfHeight;
    private float camHalfWidth;


    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
        Vector3 move = Vector3.zero;

        Vector3 mouseViewport = cam.ScreenToViewportPoint(Input.mousePosition);
        

        if (mouseViewport.x < 0 || mouseViewport.x > 1 || mouseViewport.y < 0 || mouseViewport.y > 1)
            return;

        if (mouseViewport.x <= edgeSize)
        {
            if (mouseViewport.x <= edgeSizeMax)
                move.x -= 2;
            else
                move.x -= 1;
        }

        if (mouseViewport.x >= 1f - edgeSize)
        {
            if (mouseViewport.x >= 1f - edgeSizeMax)
                move.x += 2;
            else
                move.x += 1;
        }

        if (mouseViewport.y <= edgeSize)
        {
            if (mouseViewport.y <= edgeSizeMax)
                move.y -= 2;
            else 
                move.y -= 1;
        }

        if (mouseViewport.y >= 1f - edgeSize)
        {
            if (mouseViewport.y >= 1f - edgeSizeMax)
                move.y += 2;
            else
                move.y += 1;
        }

        Vector3 newPos = transform.position + move * moveSpeed * Time.deltaTime;

        Bounds b = worldLimit.bounds;

        newPos.x = Mathf.Clamp(newPos.x, b.min.x + camHalfWidth, b.max.x - camHalfWidth);
        newPos.y = Mathf.Clamp(newPos.y, b.min.y + camHalfHeight, b.max.y - camHalfHeight);

        transform.position = newPos;
    }
}
