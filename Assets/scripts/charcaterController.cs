using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charcaterController : MonoBehaviour
{
    #region Movement
    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float dragForce;
    [SerializeField] private float startBoost;
    private float horizontalPos;
    //checks to see if we are turning or have turned 
    private bool changeDir => (rb.velocity.x > 0f && horizontalPos < 0f) || (rb.velocity.x < 0f && horizontalPos > 0f);
    #endregion


    #region Jump
    [Header("Jump")]
    [SerializeField] private float jumpforce;
    [SerializeField] private float maxJumps = 1f;
    [SerializeField] private float jumpnum;
    private bool jumpRequest;
    [SerializeField] private float gravityIncrease = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float airDragForce;
    public bool isJumping;
    //public bool hasLeftGround = false;
    [SerializeField]public bool isInAir = false;
    #endregion

    #region Double Jump
    [Header("Double Jump")]
    [SerializeField] public bool dblJump;
    #endregion

    #region Layer shit
    [Header("Layer Shit")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;
    #endregion

    #region Collison shit
    [Header("ground collison shit")]
    [SerializeField] private float groundRaycastLength;
    [SerializeField] private Vector3 _groundRaycastOffset;
    [SerializeField] private bool grounded;
    #endregion

    #region Cayote time
    [Header("cayote time")]
    [SerializeField] private float cayoteTime;
    [SerializeField] private float cayoteTimeCounter;
    #endregion

    #region Player GFX
    [Header("player GFX")]
    [SerializeField] private SpriteRenderer sr;
    #endregion

    #region other shit
    Vector2 _move;
    private Rigidbody2D rb;
    #endregion


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
      
    }

    void Update()
    {
        horizontalPos = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space) && (grounded)) || ((Input.GetKeyDown(KeyCode.Space) && cayoteTimeCounter > 0f)))
        {
            isJumping = true;
            jumpRequest = true;
            isInAir = true;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if(grounded)
        {
            cayoteTimeCounter = cayoteTime;
            
        }else
        {
            cayoteTimeCounter -= Time.deltaTime;
        }

        doubleJump();
        flipPlayer();
        
    }

    private void FixedUpdate()
    {
        movement();
        checkCollision();
        gravityFallIncrease();


        if (jumpRequest)
        {
            jumpyJump();
            jumpRequest = false;
        }

        if(grounded)
        {
            jumpnum = maxJumps;
            horizontalDrag();
        }else
        {
            verticalDrag();
        }

        
    }

    public void movement()
    {
        float xpos;
        xpos = Input.GetAxisRaw("Horizontal");


        _move = new Vector2(horizontalPos, 0f);
        _move *= (moveAcceleration);

        //to make beinging moevment snappy 
        if (rb.velocity.magnitude == 0)
        {
            rb.AddForce(_move * startBoost);
        }
        else
        {
            rb.AddForce(_move);
        }

        //checks to see what velcoity the character is at, if it is bigger or equal to max move speed it then clamps it to the max move speed 

        if (Mathf.Abs(rb.velocity.x) >= maxMoveSpeed)
        {
            Debug.Log("AT MAX MOVE SPEED");
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);
            Debug.Log("VELOCITY IS " + rb.velocity.x);
        }

    }

    public void horizontalDrag()
    {
        if ((horizontalPos == 0) && (grounded == true) || (changeDir))   //change xpos to horizontalPos
        {
            rb.drag = dragForce;
        }
        else if ((horizontalPos != 0) || (Mathf.Abs(rb.velocity.y) > 5)) //change xpos to horizontalPos
        {
            //so if it is jumping it sets the drag force to zero
            rb.drag = 0f;

        }
    }

    public void verticalDrag()
    {
        rb.drag = airDragForce;
    }

    public void jumpyJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        GetComponentInChildren<TrailRenderer>().enabled = true;

        if (!grounded)
        {
            jumpnum--;
        }

        if ((Input.GetKeyUp(KeyCode.Space)) && (rb.velocity.y > 0f))
        {
            cayoteTimeCounter = 0f;
            
        }

    }

    private void doubleJump()
    {
        if(dblJump)
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && (jumpnum > 0) && (!grounded))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);

                if (!grounded)
                {
                    jumpnum--;
                }
            }
        }
        
    }

    public void gravityFallIncrease()
    {
        if (rb.velocity.y < 0)
        {
            //checks to see if the player is falling if so makes them fall faster for a heavier feel
            rb.gravityScale = gravityIncrease;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            //if the player has stopped pressing jump and applies gravity to fall faster 
            rb.gravityScale = lowJumpMultiplier;

        }else
        {
            rb.gravityScale = 1f;
        }

    }

    //trail thingy is in here 
    public void checkCollision()
    {
        grounded = Physics2D.Raycast(transform.position + _groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer) ||

                                Physics2D.Raycast(transform.position - _groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer);


        switch (grounded)
        {
            case true:
                isInAir = false;
                break;
            case false:
                isInAir = true;
                break;
        }
        if(grounded == true)
        {
            //GetComponentInChildren<TrailRenderer>().enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position + _groundRaycastOffset, transform.position + _groundRaycastOffset + Vector3.down * groundRaycastLength);

        Gizmos.DrawLine(transform.position - _groundRaycastOffset, transform.position - _groundRaycastOffset + Vector3.down * groundRaycastLength);

        //Gizmos.DrawLine(transform.position + _rightRaycastOffset, transform.position + _rightRaycastOffset + Vector3.right * rightRaycastLength);

        //Gizmos.DrawLine(transform.position + _rightRaycastOffset, transform.position + _rightRaycastOffset + Vector3.down * rightRaycastLength);

        //Gizmos.DrawLine(transform.position + _leftRaycastOffset, transform.position + _leftRaycastOffset + Vector3.right * leftRaycastLength);

        //Gizmos.DrawLine(transform.position + _leftRaycastOffset, transform.position + _leftRaycastOffset + Vector3.right * leftRaycastLength);

    }

    private void flipPlayer()
    {
        if(rb.velocity.x > 0f)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x < 0f)
        {
            sr.flipX = true;
        }
    }

   
}
