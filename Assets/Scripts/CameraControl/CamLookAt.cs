using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAt : MonoBehaviour
{
    private Transform playerTr;

    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
    }


    void FixedUpdate () 
    {
        transform.LookAt(playerTr, Vector3.up); 
    }
}
