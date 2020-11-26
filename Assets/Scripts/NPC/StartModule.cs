using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartModule : MonoBehaviour
{
    [SerializeField] Dialog dialogPr = null;
    private Dialog dialog;
    [SerializeField] string[] sentences1;
    [SerializeField] string[] sentences2;
    [SerializeField] bool ready = false;
    public NPC astronaut2;

    private PlayerObjects playerObjects;
    private PlayerEquipment playerEquipment;
    private GameObject player;

    [SerializeField] float speed;
    [SerializeField] bool moving = false;
    public GameObject flames;

    public GameObject gameOverPanel;

    void Start()
    {
        playerObjects = GameObject.Find("Player").GetComponent<PlayerObjects>();
        playerEquipment = GameObject.Find("Player").GetComponent<PlayerEquipment>();
        player = playerObjects.gameObject;
    }

    void Update()
    {
        if (astronaut2.questCompleted)
        {
            ready = true;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (dialog != null)
            {
                Destroy(dialog);
                moving = true;
                player.SetActive(false);
                player.transform.parent = transform;
                flames.SetActive(true); //Engines start VFX
                
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            if (dialog != null)
            {
                Destroy(dialog);
            }
        }

        if (moving)
        {
            transform.Translate(Vector3.up * speed * speed * Time.deltaTime);
            if (transform.position.y >= 50)
            {
                Time.timeScale = 0;
                gameOverPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Mission Complete! Congratulations!";
                gameOverPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player") && !playerEquipment.IsSelenit && playerObjects.Scafandr && playerObjects.Helmet) 
        {
            if (!ready)
            {
                dialog = Instantiate(dialogPr);
                dialog.SetSentences(sentences1);
            }
            else
            {
                dialog = Instantiate(dialogPr);
                dialog.SetSentences(sentences2);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(dialog);
        }
    }

}
