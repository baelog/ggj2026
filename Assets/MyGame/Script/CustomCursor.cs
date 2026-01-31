using UnityEngine;
using UnityEngine.UIElements;

public class CustomCursor : MonoBehaviour
{
    public GameObject objectPrefab;

    public float rotationStep = 15f;

    private GameObject previewInstance;
    private Camera mainCamera;
    private float currentYRotation = 0f;
    void Start()
    {
        mainCamera = Camera.main;
        previewInstance = Instantiate(objectPrefab);
        previewInstance.SetActive(false);
    }

    void Update()
    {
        HandlePreview();
        HandleRotation();
    }

    void HandlePreview()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (!previewInstance.activeInHierarchy)
                previewInstance.SetActive(true);

            previewInstance.transform.position = hit.point;
            previewInstance.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        }
    }

    void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentYRotation += rotationStep;
        }
    }
}
