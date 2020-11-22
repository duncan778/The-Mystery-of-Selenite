using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMotion
{
    private Transform[] wayPoints;
    private bool cycled;
    private GameObject npc;
    private float speed;

    private int pointsCount;
    private int targetIndex;
    private Transform targetTransform;

    public CurveMotion(GameObject npc, Transform[] wayPoints, bool cycled, float speed)
    {
        this.cycled = cycled;
        this.npc = npc;
        this.speed = speed;
        pointsCount = wayPoints.Length;
        this.wayPoints = new Transform[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            this.wayPoints[i] = wayPoints[i];
        }

        //MoveInit
        targetIndex = 0;
        targetTransform = this.wayPoints[targetIndex];
        lookAtWayPoint();
    }

    public void Move()
    {
        npc.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        lookAtWayPoint();
        Vector3 wayVector = targetTransform.position - npc.transform.position;
        float wayLength = wayVector.sqrMagnitude;
        if (wayLength <= 0.1f)
        {
            targetIndex++;
            if (targetIndex == pointsCount)
            {
                targetIndex = 0;
            }
            targetTransform = this.wayPoints[targetIndex];
        }
    }

    private void lookAtWayPoint()
    {
        targetTransform.position = new Vector3(targetTransform.position.x,
                                               npc.transform.position.y,
                                               targetTransform.position.z);
        npc.transform.LookAt(targetTransform);
    }
}
