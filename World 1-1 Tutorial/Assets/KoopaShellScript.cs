using UnityEngine;
using System.Collections;

public class KoopaShellScript : MonoBehaviour
{
    public bool Moving;
    public float rayDis;
    public LayerMask EnemyMask;

    bool shellLeft, shellRight, rightBlocked, leftBlocked;
    BoxCollider rayHold;
    BoxCollider2D bCol;
    ConstantForce2D cf;
    float myWidth, myHeight;
    Rigidbody2D rb;
    SpriteRenderer mySprite;
    Transform myTrans;

    void Start()
    {
        // Short Handed variables assigned to previously set values... to save time
        myTrans = this.transform;
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        mySprite = this.gameObject.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }

    void Update()
    {
        // Shorthanded Variable that is constantly updated;
        cf = gameObject.GetComponent<ConstantForce2D>();

        // Ignore Collision Collision between the enemy and the level boundaries
        Physics2D.IgnoreLayerCollision(8, 11);
        Physics2D.IgnoreLayerCollision(9, 11);

        //Casting a horizontal line that moves with the enemy object
        Vector2 LineCastPos = (myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight * 1.2f);

        // Visual Representation of the line we just created
        Debug.DrawLine(LineCastPos, LineCastPos - myTrans.right.toVector2() * 1.2f);
    }
}
