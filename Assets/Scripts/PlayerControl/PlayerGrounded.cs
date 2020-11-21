using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : PlayerManager
{
    [SerializeField] float groundDistance = 0.2f;
    Transform groundCheck;
    public LayerMask groundMask;

    private void FixedUpdate() 
    {
        isOnGround = Physics.CheckSphere(new Vector3(transform.position.x, 
                                                    transform.position.y - 0.95f,
                                                    transform.position.z), groundDistance, groundMask);
    }

}
