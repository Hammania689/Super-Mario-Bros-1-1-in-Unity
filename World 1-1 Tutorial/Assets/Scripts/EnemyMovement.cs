using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
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

    void FixedUpdate()
    {
        // Ignore Collision Collision between the enemy and the level boundaries
        Physics2D.IgnoreLayerCollision(8, 9);

        //Casting a horizontal line that moves with the enemy object
        Vector2 LineCastPos = (myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight * 1.2f);

        //Visual Representation of the line being casted
        Debug.DrawLine(LineCastPos, LineCastPos - myTrans.right.toVector2() * 1.2f);

        // A flag to determine when the enemy needs to turn around
        bool isBlocked = Physics2D.Linecast(LineCastPos, LineCastPos - myTrans.right.toVector2() * 1.2f, EnemyMask);

        if(isBlocked)  // if the enemy hits something within the established critea 
        {
            //Then the enemy is turned in the oppositte direction
            Vector2 curRot = myTrans.eulerAngles;
            curRot.y += 180;
            myTrans.eulerAngles = curRot;

        }

        //Define a new vector for our enemy's directional movement
        Vector2 myVel = rb.velocity; 
        myVel.x = -myTrans.right.x * speed;
        rb.velocity = myVel;
       
    }
}
