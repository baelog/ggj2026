using UnityEngine;
using UnityEngine.UIElements;

public class CustomCursor : MonoBehaviour
{
    void Update()
    {
        HandlePreview();
        HandleRotation();
    }

    void HandlePreview()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -1;
        transform.position = mousePosition;
        transform.rotation = Quaternion.Euler(0, 0, 60 * GameManager.Instance.rotation);
    }

    void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.Rotate();
        }
    }
}
