using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraControl : MonoBehaviour
{

    [Header("Cam")]
    [SerializeField] private Camera cam;

    private float ogCamPos;
    private Vector3 pos = new Vector3(-0.1660494f, -0.49f, -10f);
    float counter = 0;
    public float MoveRate;
    public float moveDownDistance;
    float lastDirection = 1f;

    // Start is called before the first frame update
    Vector3 ogpos;
    void Start()
    {
        ogCamPos = cam.transform.position.y;
        ogpos = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()

    {

        if (Input.GetKey(KeyCode.S))
        {
            lastDirection = -1f;
            counter += (Time.deltaTime* MoveRate);
            if(counter >= 1)
            {
                counter = 1;
            }
            cam.transform.localPosition = Vector3.Lerp(ogpos, new Vector3(ogpos.x, ogpos.y - moveDownDistance, ogpos.z), counter);
        }
        else
        {
            if(counter >= 0)
            {
                counter -= (Time.deltaTime * MoveRate);
            }
            if(counter < 0)
            {
                counter = 0;
            }
            cam.transform.localPosition = Vector3.Lerp(ogpos, new Vector3(ogpos.x, ogpos.y + lastDirection, ogpos.z), counter);

        }

        
        
    }
}
