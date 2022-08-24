using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerAnim : MonoBehaviour
{

    [Header("Particles")]
    //[SerializeField] private ParticleSystem runParticle;

   
    private GameObject player;

    charcaterController cc;

    private Rigidbody2D rb;


    private float ogX;
    private float ogY;

    [SerializeField] private Transform transform;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip landSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        cc = player.GetComponent<charcaterController>();
        rb = player.GetComponent<Rigidbody2D>();
        ogX = transform.localScale.x;
        ogY = transform.localScale.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.isJumping)
        {
            jumping();
        }
    }



    private void land()
    {
        Sequence landSequence = DOTween.Sequence();
        landSequence.Append(transform.DOScaleX(1.8f, 0.08f));
        landSequence.Join(transform.DOScaleY(0.8f, 0.08f));
        landSequence.Append(transform.DOScaleX(ogX, 0.08f));
        landSequence.Join(transform.DOScaleY(ogY, 0.08f));
    }

    private void jumping()
    {
        Sequence jumpSequence = DOTween.Sequence();
        jumpSequence.Append(transform.DOScaleY(1.58f, 0.05f));
        jumpSequence.Join(transform.DOScaleX(0.9f, 0.05f));
        jumpSequence.Append(transform.DOScaleX(ogX, 0.05f));
        jumpSequence.Join(transform.DOScaleY(ogY, 0.05f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {

            if (cc.isInAir)
            {
                land();
                Debug.Log("LANDED ANIM SHOULD PLAY!");
            }

        }
    }

   

}
