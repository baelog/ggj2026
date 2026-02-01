using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    /**
     * <summary>
     * Speed
     * </summary>
     */
    private int collisionCount = 0;
    private Rigidbody thisRb;

    public bool IsNotColliding
    {
        get { return collisionCount == 0; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("entre");
        if (other.tag == Tags.Belt)
        {

            collisionCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Tags.Belt)
        {

            collisionCount--;
        }
        //collisionCount--;
        //Debug.Log("colision enter");
        //Debug.Log(collisionCount);
        if (collisionCount == 0)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(0, 0);
        }
    }


    private int _pullingBelt = 0;

    public int pullingBelt
    {
        set
        {
            this._pullingBelt = value;
        }

        get
        {
            return this._pullingBelt;
        }
    }

    public string name = "Rock";


    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

}
