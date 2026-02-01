using UnityEngine;

public class Store : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null || other.tag != Tags.Item) return;

        ObjectMoving itemBehaviour = other.GetComponent<ObjectMoving>();

        switch (itemBehaviour.name)
        {
            case "Rock Mask":
                GameManager.Instance.IncrementGold(3);
                GameManager.Instance.IncrementMask(1);
                break;
            case "Iron Mask":
                GameManager.Instance.IncrementGold(10);
                break;
            default:
                // code block
                break;
        }

        Destroy(other.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
