using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerMovingCC : PlayerManager
{

    Transform cameraTr;

    void Start()
    {
        cameraTr = GameObject.Find("Main Camera").transform;
    }

    void FixedUpdate()
    {
        if (!IsGameOver)
        {
            float x = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (x != 0 || v != 0)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(x, 0, v), Vector3.up);
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 
                                                      transform.eulerAngles.y + cameraTr.eulerAngles.y, 
                                                      transform.eulerAngles.z);
            }

            playerCC.Move(transform.forward * (float)Math.Sqrt(x * x + v * v) * speed * Time.deltaTime);

            if ((v != 0 || x != 0) && !Input.GetKeyDown(KeyCode.Space))
                {
                    playerAn.SetBool("Walk", true);
                    // StepSound();
                }
            else if (!Input.GetKeyDown(KeyCode.Space))
            {
                playerAn.SetBool("Walk", false);
            }
            
        }
        else
        {
            playerAn.SetBool("Walk", false);
        }
        
    }



}
