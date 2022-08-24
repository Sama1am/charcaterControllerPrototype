using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edgeDection : MonoBehaviour
{


    [Header("force values")]
    public bool canEdgeDetect;
    [SerializeField] private float edgeForce;
    [SerializeField] private float edgeForceUp;

    [Header("right detection")]
    [SerializeField] private float rightRayCastLength2;
    [SerializeField] private Vector3 rightRaycastOffset2;
    [SerializeField] private float rightRayCastLength;
    [SerializeField] private Vector3 rightRaycastOffset;

    [Header("left detection")]
    [SerializeField] private float leftRayCastLength2;
    [SerializeField] private Vector3 leftRaycastOffset2;
    [SerializeField] private float leftRayCastLength;
    [SerializeField] private Vector3 leftRaycastOffset;



    private bool colLeft;
    private bool colRight;



    [SerializeField] private LayerMask cornerCorrectLayer;

    private Rigidbody2D rb;
    charcaterController cc;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        cc = player.GetComponent<charcaterController>();
        rb = player.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void FixedUpdate()
    {
        
        if (canEdgeDetect)
        {
            CheckCollisions();


            if((colLeft))
            {
                rb.AddForce(Vector2.right * edgeForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * edgeForce, ForceMode2D.Impulse);
                

            }

            if ((colRight))
            {
                rb.AddForce(Vector2.left * edgeForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * edgeForce, ForceMode2D.Impulse);
                
            }
        }
       
    }


    //void CornerCorrect()
    //{
        
    //    RaycastHit2D _hit = Physics2D.Raycast(transform.position - leftRaycastOffset2 + Vector3.left * leftRayCastLength2, Vector3.left, leftRayCastLength2, cornerCorrectLayer); ;
    //   // _hit = Physics2D.Raycast(transform.position - leftRaycastOffset2 + Vector3.up * leftRayCastLength, Vector3.left, leftRayCastLength, cornerCorrectLayer);

    //    if (_hit.collider != null)
    //    {
    //        Debug.Log("hit the platform");

    //        rb.AddForce(Vector2.right * edgeForce, ForceMode2D.Impulse);


    //        //float _newPos = Vector3.Distance(new Vector3(_hit.point.x, transform.position.y, 0f) + Vector3.up * leftRayCastLength2,
    //        //    transform.position - leftRaycastOffset2 + Vector3.up * leftRayCastLength2);
    //        //transform.position = new Vector3(transform.position.x + _newPos, transform.position.y, transform.position.z);
    //        //rb.velocity = new Vector2(rb.velocity.x, Yvelocity * 2);
    //        //return;
    //    }

    //    //Push player to the left
    //    _hit = Physics2D.Raycast(transform.position + rightRaycastOffset2 + Vector3.right * rightRayCastLength2, Vector3.right, rightRayCastLength2, cornerCorrectLayer);
    //    if (_hit.collider != null)
    //    {
    //        rb.AddForce(Vector2.left * edgeForce, ForceMode2D.Impulse);

    //        //float _newPos = Vector3.Distance(new Vector3(_hit.point.x, transform.position.y, 0f) + Vector3.up * rightRayCastLength2,
    //        //    transform.position + rightRaycastOffset2 + Vector3.up * rightRayCastLength2);
    //        //transform.position = new Vector3(transform.position.x - _newPos, transform.position.y, transform.position.z);
    //        //rb.velocity = new Vector2(rb.velocity.x, Yvelocity * 2);
    //    }
    //}


    private void CheckCollisions()
    {

        colRight = Physics2D.Raycast(transform.position + rightRaycastOffset2, Vector2.right, rightRayCastLength2, cornerCorrectLayer) ||
                   Physics2D.Raycast(transform.position + rightRaycastOffset, Vector2.right, rightRayCastLength, cornerCorrectLayer);

        colLeft = Physics2D.Raycast(transform.position + leftRaycastOffset2, Vector2.right, leftRayCastLength2, cornerCorrectLayer) ||
                  Physics2D.Raycast(transform.position + leftRaycastOffset, Vector2.right, leftRayCastLength, cornerCorrectLayer);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

       
        Gizmos.DrawLine(transform.position + rightRaycastOffset2, transform.position + rightRaycastOffset2 + Vector3.right * rightRayCastLength2);
        Gizmos.DrawLine(transform.position + rightRaycastOffset, transform.position + rightRaycastOffset + Vector3.right * rightRayCastLength);


        Gizmos.DrawLine(transform.position + leftRaycastOffset2, transform.position + leftRaycastOffset2 + Vector3.right * leftRayCastLength2);
        Gizmos.DrawLine(transform.position + leftRaycastOffset, transform.position + leftRaycastOffset + Vector3.right * leftRayCastLength);
    }
}
