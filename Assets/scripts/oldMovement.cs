using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldMovement : MonoBehaviour
{
    [Header("Movement")]
    public GameObject playerGFX;
    public float moveSpeed = 10f;

    Vector3 _move;


    [Header("Jump")]
    public bool dblJump = false;
    public float jumpforce;
    public bool grounded = false;
    public float jumpnum;
    private float maxJumps = 1f;

    Rigidbody2D rb;
    [SerializeField] SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        //flipPlayer();
    }



    private void move()
    {
        float xpos;
        xpos = Input.GetAxisRaw("Horizontal");
        _move = new Vector3(xpos, 0f, 0f);
        _move *= (moveSpeed * Time.deltaTime);
        transform.position += _move;
    }

    public void jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                jumpnum = 0;
                rb.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);

                grounded = false;

            }
            else if (dblJump && jumpnum < maxJumps && grounded == false)
            {

                jumpnum++;
                rb.AddForce(new Vector2(0f, jumpforce / 2), ForceMode2D.Impulse);


            }



        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
    }


    private void flipPlayer()
    {
        if (rb.velocity.x > 0f)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x < 0f)
        {
            sr.flipX = true;
        }
    }
}
