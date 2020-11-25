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

    [SerializeField] Dialog dialogPr = null;
    private Dialog dialog;

    private bool questCompleted = false;

    [SerializeField] string[] sentences;
    [SerializeField] string objectName = "";
    [SerializeField] int changeAmount = 0;
    [SerializeField] GameObject gameObjectOff = null;
    [SerializeField] GameObject gameObjectOn = null;

    private PlayerObjects playerObjects;

    void Start()
    {
        NPCAnimator = GetComponent<Animator>();
        SelenitState = NPCState.Idle;
        selenit1 = new CurveMotion(this.gameObject, wayPoints);
        selenit1.MoveSetup(speed, forward: true, cycled: true);
        selenit1.RotateToPoint(comparePoint1);
        currentTime = Time.time;
        playerObjects = GameObject.Find("Player").GetComponent<PlayerObjects>();
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
                        NPCAnimator.SetBool("Walk", true);
                        SelenitState = NPCState.Walk;
                    }
                }
                break;

                case NPCState.Talk:
                {
                    if (Input.GetKeyDown(KeyCode.Y))
                    {
                        if (playerObjects.Change(objectName, changeAmount))
                        {
                            if (gameObjectOff != null)
                            {
                                gameObjectOff.SetActive(false);
                            }
                            if (gameObjectOn != null)
                            {
                                gameObjectOn.SetActive(true);
                            }
                            questCompleted = true;
                        }
                        Destroy(dialog);
                        SelenitState = NPCState.Walk;
                        NPCAnimator.SetBool("Walk", true);
                    }
                    else if (Input.GetKeyDown(KeyCode.N))
                    {
                        Destroy(dialog);
                        SelenitState = NPCState.Walk;
                        NPCAnimator.SetBool("Walk", true);
                    }
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
