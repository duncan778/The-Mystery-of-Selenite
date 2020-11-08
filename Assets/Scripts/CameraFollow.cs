using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float maxDistance = 20;
    GameObject playerGO;

    void Start()
    {
        playerGO = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        transform.LookAt(playerGO.transform); //, transform.up);
        Vector3 targetPosition = new Vector3(playerGO.transform.position.x + cameraOffset.x,
                                        playerGO.transform.position.y + cameraOffset.y,
                                        playerGO.transform.position.z + cameraOffset.z);
        float t = (transform.position - targetPosition).magnitude / maxDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, t * Time.deltaTime);
    }
}
