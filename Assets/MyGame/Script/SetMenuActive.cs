using UnityEngine;

public class SetMenuActive : MonoBehaviour
{

    public static SetMenuActive Instance;
    private void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void hideElement()
    {
        gameObject.SetActive(false);
    }
}
