using UnityEngine;
using System.Collections;

public class KoopaShellScript : MonoBehaviour
{
    public bool Moving;
    public float rayDis;
    public LayerMask EnemyMask;
    public Transform rCast;

    bool shellLeft, shellRight, rightBlocked, leftBlocked;
    BoxCollider rayHold;
    BoxCollider2D bCol;
    ConstantForce2D cf;
    float myWidth, myHeight;
    Ray rRay, lRay;
    [SerializeField]
    RaycastHit hitinfo;
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
        Moving = false;
    }

    void Update()
    {
        // Shorthanded Variable that is constantly updated;
        cf = gameObject.GetComponent<ConstantForce2D>();

        // Ignore Collision Collision between the enemy and the level boundaries
        Physics2D.IgnoreLayerCollision(8, 11);
        Physics2D.IgnoreLayerCollision(9, 11);

        //Casting a horizontal line that moves with the enemy object
        Vector2 LineCastPos = (myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight * .67f);

        // Visual Representation of the line we just created
        Debug.DrawLine(LineCastPos, LineCastPos - myTrans.right.toVector2() * .67f);
        Debug.DrawLine(LineCastPos, LineCastPos + myTrans.right.toVector2() * .67f);

        //These flags will help us change direction and handle any 2D collision that the RayCast can not
        leftBlocked = Physics2D.Linecast(LineCastPos, LineCastPos - Vector2.right * .67f);
        rightBlocked = Physics2D.Linecast(LineCastPos, LineCastPos + Vector2.right * .67f);

        /// <summary>
        /// This section will ultilize 3D Physics until otherwise commented
        /// </summary>
        // A ray is an infinite line starting at an origin and going in some direction
        // This ray's origin is in the center of the shell and is being casted in the right direction
        rRay = new Ray(rCast.position.toVector2() - rCast.right.toVector2() * myWidth + Vector2.up *
            myHeight, myTrans.TransformDirection(Vector2.right));
        lRay = new Ray(rCast.position.toVector2() + rCast.right.toVector2() * myWidth + Vector2.up *
            myHeight, myTrans.TransformDirection(Vector2.left));

        // This gives the right a ray a visual representation in our scene
        Debug.DrawRay(rCast.position.toVector2() - rCast.right.toVector2() * myWidth + Vector2.up * myHeight, rRay.direction);
        Debug.DrawRay(rCast.position.toVector2() + rCast.right.toVector2() * myWidth + Vector2.up * myHeight, rRay.direction);

        // Boolean flags become true when objects collide with a defined Raycast distance
        // Then ShellHit is called and executed in accordance to boolean flag value
        shellRight = Physics.Raycast(lRay, out hitinfo, rayDis);
        shellLeft = Physics.Raycast(rRay, out hitinfo, rayDis);
        ShellHit();
    }

    void ShellHit()
    {
        if (shellRight)
            if (shellLeft == false)
            {
                // If shell right boolean flag returns then the shell moves to the right with a constant force
                cf.relativeForce = new Vector2(30, 0);
                Debug.Log("I have been hit on the left side by " + hitinfo);
            }

        if (shellLeft)
            if (shellRight == false)
            {
                // If shell left boolean flag returns then the shell moves to the left with a constant force
                cf.relativeForce = new Vector2(-30, 0);
                Debug.Log("I have been hit on the right side by " + hitinfo);
            }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            // if collision between the shell and enviromental prefabs happen
            // then the shell is turned around accordingly
            Debug.Log("This Koopa Shell has been flipped in the Y rotaional axis");
            Vector3 curRot = myTrans.eulerAngles;
            curRot.y += 180;
            myTrans.eulerAngles = curRot;
        }
    }

    //////////////////////////////////////////////////////////////////
    ///////////////////// 3D COLLISION ENDS //////////////////////////
    //////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////
    ///////////////////// 2D COLLISION BEGINS/////////////////////////
    //////////////////////////////////////////////////////////////////

    IEnumerator MoveShell(Collision2D coll) //Courotine for 2D Collision 
    {
        if(coll.gameObject.tag == "Player")
            if(leftBlocked == false && rightBlocked == false)
            {
                yield return new WaitForFixedUpdate();
                yield return Moving = true;
            }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (leftBlocked || rightBlocked)
        {
            if (coll.gameObject.tag == "Player" && this.Moving == false)
            {
                StartCoroutine(MoveShell(coll));
            }
        }
        if (coll.gameObject.tag == "Player" && this.Moving)
        {
            switch (PlayerController.PlayerState)
            {
                case 1:
                    PlayerController.PlayerState = 0;
                    break;
                case 2:
                    PlayerController.PlayerState = 1;
                    break;
                default:
                    Destroy(coll.gameObject);
                    Debug.Log("You just lost a life");
                    break;
            }
        }
    }
}
