using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingCC : MonoBehaviour
{
    // Animator playerAn;
    CharacterController playerCC;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravity = 10f;
    public bool IsGameOver { get; set; }
    Vector3 way;

    void Start()
    {
        // playerAn = GetComponent<Animator>();
        playerCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<PlayerGrounded>().IsOnGround)
            {   
                way = new Vector3(0, jumpForce, 0);
                // playerAn.SetTrigger("Jump");
            }
            way.y -= Time.deltaTime * gravity;
            playerCC.Move(way * Time.deltaTime);
        }
        
    }

    
}