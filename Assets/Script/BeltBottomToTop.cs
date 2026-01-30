using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BeltBottomToTop : Ibelt
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    /**
     * <summary>
     * Watch for the item on top of the belt
     * Move the items to the origin point then move it downward
     * </summary>
     */
    protected override void WatchForItem()
    {
        Bounds bounds = this.transform.GetComponent<PolygonCollider2D>().bounds;
        Vector2 size = bounds.size;
        List<Collider2D> colliders = new List<Collider2D>();
        PolygonCollider2D thisCollider = this.transform.GetComponent<PolygonCollider2D>();
        thisCollider.Overlap(this.transform.position, 0, colliders);

        //Collider2D[] colliders = Physics2D.OverlapBoxAll(this.transform.position, size, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == Tags.Item)
            {
                Debug.Log("test");
                Transform item = collider.GetComponent<Transform>();
                Bounds itemBounds = item.GetComponent<Collider2D>().bounds;
                Vector2 itemPoint = new Vector2(itemBounds.max.x, itemBounds.min.y);

                // Do not move the item if the item point is not on the belt
                if (!bounds.Contains(itemPoint))
                {
                    continue;
                }

                // Has the item reached the origin point?
                if (this.transform.position.x - item.position.x < 0)
                {
                    ObjectMoving itemBehaviour = item.GetComponent<ObjectMoving>();
                    itemBehaviour.speed = 5f;
                    itemBehaviour.MoveLeft();
                }
                // If the item has reached the origin point, move up
                else
                {
                    ObjectMoving itemBehaviour = item.GetComponent<ObjectMoving>();
                    itemBehaviour.speed = 5f;
                    itemBehaviour.MoveUp();
                }
            }
        }
    }
}
