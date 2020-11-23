using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelenitNPC : MonoBehaviour
{
    public NPCState SelenitState { get; set; }
    public Transform[] wayPoints;
    [SerializeField] float speed = 1;
    public Transform comparePoint1;

    CurveMotion selenit1;

    void Start()
    {
        SelenitState = NPCState.Walk;
        selenit1 = new CurveMotion(this.gameObject, wayPoints);
        selenit1.MoveSetup(speed, forward: true, cycled: true);
    }

    void Update()
    {
        if (true) //not game over
        {
            switch (SelenitState)
            {
                case NPCState.Walk:
                {
                    bool result = selenit1.Move(comparePoint1);
                }
                break;

                case NPCState.Work:
                {

                }
                break;

                case NPCState.Talk:
                {

                }
                break;

                case NPCState.Panic:
                {

                }
                break;

                default: break;
            }
        }
    }
}
