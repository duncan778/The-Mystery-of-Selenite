using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerMovingCC : MonoBehaviour
{
    CharacterController playerCC;
    GameObject playerVisual;
    // Animator playerAn;
    [SerializeField] float walkSpeed = 1.5f;
    public bool IsGameOver { get; set; }
    Transform cameraTr;


    void Start()
    {
        // playerAn = GetComponent<Animator>();
        playerCC = GetComponent<CharacterController>();
        playerVisual = transform.GetChild(0).gameObject;
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
                //transform.LookAt(transform.position + new Vector3(x, 0, v));
                transform.rotation = Quaternion.LookRotation(new Vector3(x, 0, v), Vector3.up);
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + cameraTr.eulerAngles.y, transform.eulerAngles.z);
            }

            playerCC.Move(transform.forward * (float)Math.Sqrt(x * x + v * v) * walkSpeed * Time.deltaTime);

            if (v != 0 || x != 0)
                {
                    // playerAn.SetBool("Walk", true);
                    // StepSound();
                }
            else
            {
                // playerAn.SetBool("Walk", false);
            }
            
        }
        else
        {
            // playerAn.SetBool("Walk", false);
        }
        
    }



}
