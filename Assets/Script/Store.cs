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
                GameManager.Instance.IncrementGold(1);
                GameManager.Instance.IncrementStoneMask(1);
                break;
            case "Iron Mask":
                GameManager.Instance.IncrementIronMask(1);
                GameManager.Instance.IncrementGold(5);
                break;
            case "Prismarine Mask":
                GameManager.Instance.IncrementPrismarineMask(1);
                GameManager.Instance.IncrementGold(10);
                break;
            case "Light Prismarine Mask":
                GameManager.Instance.IncrementLightPrismarineMask(1);
                GameManager.Instance.IncrementGold(25);
                break;
            case "Solidified Prismarine Mask":
                GameManager.Instance.IncrementSolidifiedPrismarineMask(1);
                GameManager.Instance.IncrementGold(50);
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
