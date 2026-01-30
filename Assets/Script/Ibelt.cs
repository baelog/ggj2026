using UnityEngine;

public abstract class Ibelt : MonoBehaviour
{
    /**
    * <summary>
    * The speed of how fast the item will move
    * </summary>
    */
    protected float speed = 5f;

    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.WatchForItem();
    }

    /**
     * <summary>
     * Watch for any item on top of the belt,
     * if there is, move it.
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    protected abstract void WatchForItem();
}