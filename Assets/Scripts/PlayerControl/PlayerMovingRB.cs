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
    [SerializeField] float rotSpeed = 1f;
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

            playerRb.AddForce(playerVisual.transform.forward * Math.Abs(v) * walkSpeed * Time.deltaTime);
            playerRb.AddForce(playerVisual.transform.forward * Math.Abs(x) * walkSpeed * Time.deltaTime);

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
