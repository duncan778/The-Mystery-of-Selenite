using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerMovingRB : MonoBehaviour
{
    Rigidbody playerRb;
    GameObject playerVisual;
    // Animator playerAn;
    [SerializeField] float walkSpeed = 1.5f;
    public bool IsGameOver { get; set; }


    void Start()
    {
        // playerAn = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerVisual = transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        if (!IsGameOver)
        {
            float x = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (x != 0 || v != 0)
            {
                playerVisual.transform.LookAt(playerVisual.transform.position + new Vector3(x, 0, v));
            }
            else if (playerRb.velocity.magnitude > 0)
            {
                playerVisual.transform.LookAt(playerVisual.transform.position + new Vector3(playerRb.velocity.x,0,playerRb.velocity.z));
            }

            playerRb.AddForce(playerVisual.transform.forward * (float)Math.Sqrt(x * x + v * v) * walkSpeed * Time.deltaTime);

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
