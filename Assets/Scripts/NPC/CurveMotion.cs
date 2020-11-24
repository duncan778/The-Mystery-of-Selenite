using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMotion
{
    private Transform[] wayPoints;
    private bool cycled = true;
    private GameObject npc;
    private float speed = 1;
    private bool forward = true;

    private int pointsCount;
    private int targetIndex;
    private Transform targetTransform;
    private int passedIndex;


    public CurveMotion(GameObject npc, Transform[] wayPoints)
    {
        this.npc = npc;
        pointsCount = wayPoints.Length;
        this.wayPoints = new Transform[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            this.wayPoints[i] = wayPoints[i];
        }
        targetIndex = 0;
        targetTransform = this.wayPoints[targetIndex];
        passedIndex = pointsCount-1;
        LookAtWayPoint();
    }

    public void MoveSetup(float speed, bool forward = true, bool cycled = true)
    {
        this.cycled = cycled;
        this.speed = speed;
        if (forward != this.forward)
        {
            ChangeDirection();
        }
        this.forward = forward;
    }

    private void ChangeDirection()
    {
        int tempIndex = targetIndex;
        targetIndex = passedIndex;
        passedIndex = tempIndex;
        targetTransform = wayPoints[targetIndex];
        LookAtWayPoint();
    }

    public bool Move(Transform comparePoint = null)
    {
        bool targetCompleted = false;
        npc.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        LookAtWayPoint();
        Vector3 wayVector = targetTransform.position - npc.transform.position;
        float wayLength = wayVector.sqrMagnitude;
        if (wayLength <= 0.01f)
        {
            if (forward)
            {
                passedIndex = targetIndex;
                targetIndex++;

                if (cycled)
                {
                    if (targetIndex == pointsCount)
                    {
                        targetIndex = 0;
                        passedIndex = pointsCount-1;
                    }
                }
                else
                {
                    //ChangeDirection
                    if (targetIndex == pointsCount)
                    {
                        targetIndex = pointsCount-2;
                        passedIndex = pointsCount-1;
                        forward = false;
                    }
                }
            }
            else //backward
            {
                passedIndex = targetIndex;
                targetIndex--;

                if (cycled)
                {
                    if (targetIndex == -1)
                    {
                        targetIndex = pointsCount-1;
                        passedIndex = 0;
                    }
                }
                else
                {
                    //ChangeDirection
                    if (targetIndex == -1)
                    {
                        targetIndex = 1;
                        passedIndex = 0;
                        forward = true;
                    }
                }
            }
        targetTransform = wayPoints[targetIndex];

        if (wayPoints[passedIndex] == comparePoint)
            targetCompleted = true;
        }

        return targetCompleted;
    }

    private void LookAtWayPoint()
    {
        targetTransform.position = new Vector3(targetTransform.position.x,
                                               npc.transform.position.y,
                                               targetTransform.position.z);
        npc.transform.LookAt(targetTransform);
    }

    public void RotateToPoint(Transform comparePoint1)
    {
        npc.transform.rotation = comparePoint1.rotation;
    }

}
