using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelenitNPC : MonoBehaviour
{
    public NPCState SelenitState { get; set; }
    public Transform[] wayPoints;
    [SerializeField] float speed = 1;

    public Transform comparePoint1;

    private float currentTime;
    [SerializeField] float idleTime = 3;

    CurveMotion selenit1;
    private Animator NPCAnimator;

    [SerializeField] Dialog dialogPr;
    private Dialog dialog;

    private bool questCompleted = false;

    [SerializeField] string[] sentences;

    void Start()
    {
        NPCAnimator = GetComponent<Animator>();
        SelenitState = NPCState.Idle;
        selenit1 = new CurveMotion(this.gameObject, wayPoints);
        selenit1.MoveSetup(speed, forward: true, cycled: true);
        selenit1.RotateToPoint(comparePoint1);
        currentTime = Time.time;
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
                    if (result)
                    {
                        SelenitState = NPCState.Idle;
                        selenit1.RotateToPoint(comparePoint1);
                        NPCAnimator.SetBool("Walk", false);
                        currentTime = Time.time;
                    }
                }
                break;

                case NPCState.Idle:
                {
                    if (Time.time - currentTime > idleTime)
                    {
                        // selenit1.MoveSetup(speed, forward: true, cycled: true);
                        NPCAnimator.SetBool("Walk", true);
                        SelenitState = NPCState.Walk;
                    }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !questCompleted) 
        {
            dialog = Instantiate(dialogPr);
            dialog.SetSentences(sentences);
            SelenitState = NPCState.Talk;
            NPCAnimator.SetBool("Walk", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(dialog);
            SelenitState = NPCState.Walk;
            NPCAnimator.SetBool("Walk", true);
        }
    }
}
