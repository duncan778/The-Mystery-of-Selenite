using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    public bool IsOnGround { get; private set; }
    [SerializeField] float groundDistance = 0.2f;
    Transform groundCheck;
    public LayerMask groundMask;

    private void Update() 
    {
        IsOnGround = Physics.CheckSphere(new Vector3(transform.position.x, 
                                                    transform.position.y - 1,
                                                    transform.position.z), groundDistance, groundMask);
    }

}
