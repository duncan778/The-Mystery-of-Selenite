using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCState SelenitState { get; set; }
    public Transform[] wayPoints;
    [SerializeField] float speed = 1;

    public Transform workPoint;

    private float currentTime;
    [SerializeField] float pauseTime = 3;
    [SerializeField] string workAnim;

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
    private PlayerEquipment playerEquipment;

    [SerializeField] bool iSelenit;

    void Start()
    {
        NPCAnimator = GetComponent<Animator>();
        SelenitState = NPCState.Work;
        selenit1 = new CurveMotion(this.gameObject, wayPoints);
        selenit1.MoveSetup(speed, forward: true, cycled: true);
        selenit1.RotateToPoint(workPoint);
        currentTime = Time.time;
        playerObjects = GameObject.Find("Player").GetComponent<PlayerObjects>();
        playerEquipment = GameObject.Find("Player").GetComponent<PlayerEquipment>();
    }

    void Update()
    {
        if (true) //not game over
        {
            switch (SelenitState)
            {
                case NPCState.Walk:
                {
                    bool result = selenit1.Move(workPoint);
                    if (result)
                    {
                        SelenitState = NPCState.Work;
                        selenit1.RotateToPoint(workPoint);
                        NPCAnimator.SetBool("Walk", false);
                        NPCAnimator.SetBool(workAnim, true);
                        currentTime = Time.time;
                    }
                }
                break;

                case NPCState.Work:
                {
                    if (Time.time - currentTime > pauseTime)
                    {
                        NPCAnimator.SetBool(workAnim, false);
                        NPCAnimator.SetBool("Walk", true);
                        SelenitState = NPCState.Walk;
                    }
                }
                break;

                case NPCState.Idle:
                {
                    if (Time.time - currentTime > pauseTime)
                    {
                        NPCAnimator.SetBool("Walk", true);
                        NPCAnimator.SetBool(workAnim, false);
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
                                gameObjectOn.transform.position = new Vector3(transform.position.x,
                                                                              gameObjectOn.transform.position.y,
                                                                              transform.position.z);
                            }
                            questCompleted = true;
                        }
                        Destroy(dialog);
                        SelenitState = NPCState.Walk;
                        NPCAnimator.SetBool("Walk", true);
                        NPCAnimator.SetBool(workAnim, false);
                    }
                    else if (Input.GetKeyDown(KeyCode.N))
                    {
                        Destroy(dialog);
                        SelenitState = NPCState.Walk;
                        NPCAnimator.SetBool("Walk", true);
                        NPCAnimator.SetBool(workAnim, false);
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
            //Panic Test
            if (iSelenit) //for Selenit NPC
            {
                if (playerObjects.Scafandr && !playerEquipment.IsSelenit)
                {
                    //Panic Action
                    return;
                }
                
            }
            else //for Astronaut NPC
            {
                if (playerEquipment.IsSelenit || (!playerEquipment.IsSelenit && playerObjects.Scafandr && !playerObjects.Helmet)
                                              || (!playerEquipment.IsSelenit && !playerObjects.Scafandr && playerObjects.Helmet))
                {
                    //Panic action
                    return;
                }
                
            }

            dialog = Instantiate(dialogPr);
            dialog.SetSentences(sentences);
            SelenitState = NPCState.Talk;
            NPCAnimator.SetBool("Walk", false);
            NPCAnimator.SetBool(workAnim, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(dialog);
            SelenitState = NPCState.Walk;
            NPCAnimator.SetBool("Walk", true);
            NPCAnimator.SetBool(workAnim, false);
        }
    }
}
