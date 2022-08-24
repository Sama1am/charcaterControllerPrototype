using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{

    private Rigidbody2D rb;
    charcaterController cc;
    private GameObject player;

    //public Toggle dblJumpToggle;
   // public Toggle edgeToggle;

    public Slider edgeSlider;
    public Slider jumpSlider;

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

    public void exit()
    {
        Application.Quit();

    }

    public void newMove()
    {
        SceneManager.LoadScene(0);
    }

    public void oldMove()
    {
        SceneManager.LoadScene(1);
    }

    public void dblJump()
    {


        if(jumpSlider.value == 1)
        {
            cc.dblJump = true;

        }else if(jumpSlider.value == 0)
        {
            cc.dblJump = false;
        }


        //if(dblJumpToggle.isOn)
        //{
        //    cc.dblJump = true;
        //}else
        //{
        //    cc.dblJump = false;
            
        //}
       
    }

    public void edgeDect()
    {

        if(edgeSlider.value == 1)
        {
            player.GetComponent<edgeDection>().canEdgeDetect = true;
        }else if(edgeSlider.value == 0)
        {
            player.GetComponent<edgeDection>().canEdgeDetect = false;
        }

        //if(edgeToggle.isOn)
        //{
        //    player.GetComponent<edgeDection>().canEdgeDetect = true;
        //}else
        //{
        //    player.GetComponent<edgeDection>().canEdgeDetect = false;
        //}
         
    }
}
