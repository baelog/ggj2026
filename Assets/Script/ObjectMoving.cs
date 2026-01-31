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
        Debug.Log("entre");
        collisionCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collisionCount--;
        Debug.Log("colision enter");
        Debug.Log(collisionCount);
        if (collisionCount == 0)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(0,0);
        }
    }

    private float _speed = 1;

    public float speed
    {
        set
        {
            this._speed = value;
        }

        get
        {
            return this._speed;
        }
    }
    private float _pullingBelt = 0;

    public float pullingBelt
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

    /**
     * <summary>
     * The 2d collision bounds
     * </summary>
     */
    private Bounds _bounds;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {

    }

    /**
     * <summary>
     * Move left
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void MoveLeft()
    {
        Vector3 nextPosition = new Vector3(
            this.transform.position.x - this.speed * Time.deltaTime,
            this.transform.position.y,
            this.transform.position.z
        );

        this.transform.position = nextPosition;
    }

    /**
     * <summary>
     * Move right
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void MoveRight()
    {
        Vector3 nextPosition = new Vector3(
            this.transform.position.x + this.speed * Time.deltaTime,
            this.transform.position.y,
            this.transform.position.z
        );

        this.transform.position = nextPosition;
    }

    /**
     * <summary>
     * Move down
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void MoveDown()
    {
        Vector3 nextPosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y - this.speed * Time.deltaTime,
            this.transform.position.z
        );

        this.transform.position = nextPosition;
    }

    /**
     * <summary>
     * Move up
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void MoveUp()
    {
        Vector3 nextPosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y + this.speed * Time.deltaTime,
            this.transform.position.z
        );

        this.transform.position = nextPosition;
    }
}
