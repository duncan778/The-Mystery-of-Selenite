using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    // Animator playerAn;
    Rigidbody playerRb;
    [SerializeField] float jumpForce = 5f;
    bool isOnGround = true;
    public bool IsGameOver { get; set; }

    void Start()
    {
        // playerAn = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                // playerAn.SetTrigger("Jump");
                playerRb.AddForce(new Vector3(0, jumpForce, 0) , ForceMode.Impulse);
            }
        }
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}