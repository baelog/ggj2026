using UnityEngine;

public abstract class Ibelt : MonoBehaviour
{
    /**
    * <summary>
    * The speed of how fast the item will move
    * </summary>
    */
    protected float speed = 5f;
    protected static int inc = 0;

    // Use this for initialization
    protected virtual void Start()
    {
        inc++;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }
}