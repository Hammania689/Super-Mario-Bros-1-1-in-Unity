using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float height = 5f;
    public static int PlayerState;
    public bool grounded = false;
    Animator anim;
    bool movingRight;
    Rigidbody2D rb;

    void Start()
    {
        //Baseline State for the player
        PlayerState = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("movingRight", movingRight);
    }
	
	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)))
        {
            transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
            movingRight = true;
        }
        else movingRight = false;
        if(Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)))
            transform.Translate(Vector3.left * speed * Time.fixedDeltaTime); 
        if (Input.GetKeyDown(KeyCode.UpArrow)  || (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space))))
            if(grounded == true)
             rb.velocity = new Vector2 (0f, height); 
    }

    void OnTriggerStay2D( Collider2D other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
            grounded = true;
    }

    void OnTriggerExit2D( Collider2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
            grounded = false;
    }
}
