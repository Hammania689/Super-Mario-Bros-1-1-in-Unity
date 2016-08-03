using UnityEngine;
using System.Collections;

public class MushroomScript : MonoBehaviour
{

    public float speed = 2.7f;
    public LayerMask EnemyMask;

    Transform myTrans;
    float myWidth, myHeight;
    Rigidbody2D rb;
    SpriteRenderer mySprite;

    void Start()
    {
        // Short Handed variables assigned to previously set values... to save time
        myTrans = this.transform;
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        mySprite = this.gameObject.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }

    //Be able to destroy prefab and raise player state
    void OnTriggerEnter2D()
    {
        if(PlayerController.PlayerState == 0)
        {
            PlayerController.PlayerState = 1;
        }

        Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        // Ignore Collision Collision between the enemy and the level boundaries
        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(9, 10);

        //Casting a horizontal line that moves with the enemy object
        Vector2 LineCastPos = (myTrans.position.toVector2() + myTrans.right.toVector2() * myWidth + Vector2.up * myHeight * 1.2f);

        // Visual Representation of the line we just created
        Debug.DrawLine(LineCastPos, LineCastPos + myTrans.right.toVector2() * 1.2f);

        // A Boolean Flag that determines whether or not the enemy's line is blocked
        bool isBlocked = Physics2D.Linecast(LineCastPos, LineCastPos + myTrans.right.toVector2() * 1.2f, EnemyMask);

        if (isBlocked)
        {
            Vector2 currRot = myTrans.eulerAngles; // Place holder variable set equal to rotational values
            currRot.y += 180; //Place holder variable's y axis is added on to
            myTrans.eulerAngles = currRot; // Rotational values are set equal to the Place holder variables
        }

        Vector2 myVel = rb.velocity; // Place holder variable defined and set equal to Rigidbody's velocity values
        myVel.x = myTrans.right.x * speed; // Place holder variable is manipulated
        rb.velocity = myVel; // The Rigidbody's velocity is then set equal to the place holder
    }
}
