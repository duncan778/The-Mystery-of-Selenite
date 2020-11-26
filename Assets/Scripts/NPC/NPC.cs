using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC : MonoBehaviour
{
    public GameObject gameOverPanel;

    public NPCState SelenitState { get; set; }
    public Transform[] wayPoints;
    [SerializeField] float speed = 1;

    public Transform workPoint;
    public Transform panicPoint;

    private float currentTime;
    [SerializeField] float pauseTime = 3;
    [SerializeField] string workAnim;
    [SerializeField] string panicAnim;

    CurveMotion selenit1;
    CurveMotion panicWay;

    private Animator NPCAnimator;

    [SerializeField] Dialog dialogPr = null;
    private Dialog dialog;

    [SerializeField] bool questCompleted = false;

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
                    Debug.Log("Panic");
                    bool result = panicWay.Move(panicPoint);
                    if (result)
                    {
                        gameObject.SetActive(false);
                        Time.timeScale = 0;
                        gameOverPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "You were discovered! Panic arose. Mission failed";
                        gameOverPanel.SetActive(true);
                    }
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
            if (SelenitState == NPCState.Panic)
            {
                return;
            }

            //Panic Test
            if (iSelenit) //for Selenit NPC
            {
                if (playerObjects.Scafandr && !playerEquipment.IsSelenit)
                {
                    //Panic Action
                    Transform[] panicPoints = new Transform[] {this.gameObject.transform, panicPoint};
                    panicWay = new CurveMotion(this.gameObject, panicPoints);
                    panicWay.MoveSetup(speed * 2, forward: true, cycled: false);
                    SelenitState = NPCState.Panic;
                    NPCAnimator.SetBool("Walk", false);
                    NPCAnimator.SetBool(workAnim, false);
                    NPCAnimator.SetBool(panicAnim, true);
                    return;
                }
                
            }
            else //for Astronaut NPC
            {
                if (playerEquipment.IsSelenit || (!playerEquipment.IsSelenit && playerObjects.Scafandr && !playerObjects.Helmet)
                                              || (!playerEquipment.IsSelenit && !playerObjects.Scafandr && playerObjects.Helmet))
                {
                    //Panic action
                    Transform[] panicPoints = new Transform[] {this.gameObject.transform, panicPoint};
                    panicWay = new CurveMotion(this.gameObject, panicPoints);
                    panicWay.MoveSetup(speed * 2, forward: true, cycled: false);
                    SelenitState = NPCState.Panic;
                    NPCAnimator.SetBool("Walk", false);
                    NPCAnimator.SetBool(workAnim, false);
                    NPCAnimator.SetBool(panicAnim, true);
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
        if (SelenitState == NPCState.Panic)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Destroy(dialog);
            SelenitState = NPCState.Walk;
            NPCAnimator.SetBool("Walk", true);
            NPCAnimator.SetBool(workAnim, false);
        }
    }
}
