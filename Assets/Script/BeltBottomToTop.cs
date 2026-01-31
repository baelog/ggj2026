using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BeltBottomToTop : Ibelt
{

    public float moveSpeed = 2f;
    public float centerThreshold = 0.05f;
    public float rotation = 0f;
    private int id;

    // Top-left direction (pointy-top hex)
    private Vector2 outDirection;

    // Use this for initialization
    protected override void Start()
    {
        rotation = GameManager.Instance.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 60 * rotation);
        float angle = (3.0f + (rotation * 2)) * Mathf.PI / 6;
        outDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        base.Start();
        id = inc;
        //Debug.Log(id);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null || other.tag != Tags.Item) return;

        Vector2 center = transform.position;
        Vector2 currentPos = rb.position;

        Vector2 toCenter = center - currentPos;

        // If not yet centered  pull to center
        ObjectMoving itemBehaviour = other.GetComponent<ObjectMoving>();

        if (toCenter.magnitude > centerThreshold && id != itemBehaviour.pullingBelt)
        {
            //if (states == 0)
            //{
                rb.linearVelocity = toCenter.normalized * moveSpeed;
            //}
        }
        // Once centered  move out
        else
        {
            rb.linearVelocity = outDirection * moveSpeed;
            itemBehaviour.pullingBelt = id;
        }
    }

}
