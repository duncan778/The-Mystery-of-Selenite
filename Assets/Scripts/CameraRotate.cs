using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Transform playerTr;
    private Vector3  MousePos;
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
        MyAngle = 0;
        // расчитываем угол, как:
        // разница между позицией мышки и центром экрана, делённая на размер экрана
        //  (чем дальше от центра экрана тем сильнее поворот)
        // и умножаем угол на чуствительность из параметров
        MyAngle = sensitivity*((MousePos.x-(Screen.width/2))/Screen.width);
        transform.RotateAround(playerTr.position, playerTr.up, MyAngle);
        // MyAngle = sensitivity*((MousePos.y-(Screen.height/2))/Screen.height);
        // transform.RotateAround(playerTr.position, transform.right, -MyAngle);
    }
}
