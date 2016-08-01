using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float height = 5f;
    public static int PlayerState;
    public bool grounded = false;
    public Color BaseColor;
    public Color FPowerColor;


    Rigidbody2D rb;
    Renderer pc;

    void Start()
    {
        //Baseline State for the player
        PlayerState = 0;

        // Sets Rigidbody component to rb
        rb = GetComponent<Rigidbody2D>();

        //Sets Renderer component to pc
        pc = gameObject.GetComponent<Renderer>() ;
    }

    void State()
    {
        if(PlayerState == 0)
        {
            pc.material.color = BaseColor;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(PlayerState == 1)
        {
             pc.material.color = BaseColor;
            transform.localScale = new Vector3(1f, 1.5f, 1f);
        }

        if(PlayerState == 2)
        {
            pc.material.color = FPowerColor;
        }

    }
	
	void FixedUpdate ()
    {
	    if(Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)))
        { transform.Translate(Vector3.right * speed * Time.fixedDeltaTime); }

        if(Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)))
        { transform.Translate(Vector3.left * speed * Time.fixedDeltaTime); }

        if (Input.GetKeyDown(KeyCode.UpArrow)  || (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space))))
        {
            if(grounded == true)
            {  rb.velocity = new Vector2 (0f, height); }   
        }

        State();
    }

    void OnTriggerEnter2D( Collider2D other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            grounded = true;
        }


    }

    void OnTriggerExit2D( Collider2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            grounded = false;
        }
    }
}
