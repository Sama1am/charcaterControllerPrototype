using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterJump : MonoBehaviour
{

    [Header("Jump")]
    public float fallMultuplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if(rb.velocity.y < 0 )
        {
            //checks to see if the player is falling if so makes them fall faster for a heavier feel
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultuplier - 1) * Time.deltaTime;

        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            //checks to see if player is holidng down jump if so the player will jump higher 
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }

    }
}
