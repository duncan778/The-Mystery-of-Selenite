using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingCC : MonoBehaviour
{
    Animator playerAn;
    CharacterController playerCC;
    
    private GameObject playerAvatar;
    public GameObject PlayerAvatar
    {
        get { return playerAvatar; }
        set { playerAvatar = value; 
            playerAn = playerAvatar.GetComponent<Animator>();
            }
    }

    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravity = 10f;
    public bool IsGameOver { get; set; }
    Vector3 way;

    void Start()
    {
        playerAn = PlayerAvatar.GetComponent<Animator>();
        playerCC = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (!IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<PlayerGrounded>().IsOnGround)
            {   
                playerAn.SetTrigger("Jump");
                way = new Vector3(0, jumpForce, 0);
            }
            way.y -= Time.deltaTime * gravity;
            playerCC.Move(way * Time.deltaTime);
        }
        
    }

    
}