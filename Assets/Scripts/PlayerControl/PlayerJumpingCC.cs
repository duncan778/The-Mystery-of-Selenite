using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingCC : PlayerManager
{
    
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravity = 10f;
    Vector3 way;

    void FixedUpdate()
    {
        if (!IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {   
                playerAn.SetTrigger("Jump");
                way = new Vector3(0, jumpForce, 0);
            }
            way.y -= Time.deltaTime * gravity;
            playerCC.Move(way * Time.deltaTime);
        }
        
    }

    
}