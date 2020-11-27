using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerMovingCC : PlayerManager
{

    public AudioClip leftStepSound;
    public AudioClip rightStepSound;
    private bool leftStep = false;
    public float StepTime { get; set; } = 0.24f;
    float currentTime;

    Transform cameraTr;

    void Start()
    {
        cameraTr = GameObject.Find("Main Camera").transform;
        currentTime = Time.time;
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
                    StepSound();
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

    void StepSound()
    {
        if (Time.time-currentTime > StepTime)
        {
            if (leftStep)
            {
                playerSFX.PlayOneShot(rightStepSound, 0.05f);
            }
            else
            {
                playerSFX.PlayOneShot(leftStepSound, 0.05f);
            }
        currentTime = Time.time;
        }
        
    }

}
