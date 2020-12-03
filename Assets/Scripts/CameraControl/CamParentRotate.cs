using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamParentRotate : MonoBehaviour
{
    private Transform playerTr;
    private Vector3  MousePos;
    private Vector3  lastMousePos;
    [SerializeField] float   sensitivity = 1F;
    private float   MyAngle = 0F;

    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        MousePos = Input.mousePosition;
    }

    void FixedUpdate () 
    {
        MyAngle = sensitivity*((MousePos.x-(Screen.width/2))/Screen.width);
        transform.RotateAround(playerTr.position, playerTr.up, MyAngle);
    }
}
