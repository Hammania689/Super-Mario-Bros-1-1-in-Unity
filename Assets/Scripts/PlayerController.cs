using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static int PlayerState;

    [SerializeField] SoundFxManager sfx;
    [SerializeField] AudioClip defaultJumpSfx;
    [SerializeField] AudioClip bigJumpSfx;
    [SerializeField] AudioClip coinSfx;

    Animator anim;
    bool movingRight, movingLeft, walking, jumped,
        jumping, grounded = false;
    float speed = 5f, height = 8.7f, jumpTime, walkTime;
    int moveState;
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
        State();
        //
        //
        //
        if(Pause.gamePaused == true)
            speed = 0;
    }
	
	void FixedUpdate ()
    {
        if (!Input.GetKey(KeyCode.RightArrow) || !(Input.GetKey(KeyCode.D)) ||
            (!Input.GetKey(KeyCode.LeftArrow) || (!Input.GetKey(KeyCode.A))))
            moveState = 0;
        if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)))
            moveState = 1;
        if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)))
            moveState = 2;
            Jump();
    }

    void State()
    {
        switch(moveState)
        {
            case 1: Movement(Vector3.right);
                movingRight = true;
                movingLeft = false;
                break;
            case 2: Movement(Vector3.left);
                movingLeft = true;
                movingRight = false;
                break;
            default:
                movingRight = false;
                movingLeft = false;
                walking = false;
                walkTime = 0f;
                speed = 5f;
                break;
        }

        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.Space))))
            {
                jumped = true; jumping = true;
                jumpTime += Time.fixedDeltaTime;
            }
            else jumping = false;
    }

    void Movement(Vector3 dir)
    {
        walking = true;
        speed = Mathf.Clamp(speed, speed, 8f);
        walkTime += Time.deltaTime;
        transform.Translate(dir * speed * Time.fixedDeltaTime);
        if (walkTime < 3f && walking)
            speed += .025f;
        else if (walkTime > 3f)
            speed = 8f;
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.Space))))
            if (grounded == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, height);

                if (PlayerState == 0)
                    sfx.PlaySoundFX(defaultJumpSfx);
                else
                    sfx.PlaySoundFX(bigJumpSfx);
            }

        if (jumping == true && jumped == true)
            rb.gravityScale -= (.1f * Time.fixedDeltaTime);
        if (jumpTime > 1f)
            jumping = false;
        if (jumping == false && jumped == true)
            rb.gravityScale += .2f;
    }

    void OnTriggerStay2D( Collider2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            grounded = true;
            jumped = false;
            jumping = false;
            jumpTime = 0f;
            rb.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
            sfx.PlaySoundFX(coinSfx);
    }

    void OnTriggerExit2D( Collider2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
            grounded = false;
    }
}
