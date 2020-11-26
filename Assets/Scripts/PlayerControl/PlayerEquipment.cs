using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : PlayerManager
{
    public GameObject selenit;
    public GameObject scafandr;
    [SerializeField] float walkSpeed = 3.75f;
    [SerializeField] float runSpeed = 7.5f;
    private bool isSelenit;
    public bool IsSelenit
    {
        get { return isSelenit; }
        set { isSelenit = value; }
    }
    

    private PlayerObjects playerObjects;

    void Awake()
    {
        SetSelenit();
    }

    private void Start()
    {
        playerObjects = GetComponent<PlayerObjects>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (IsSelenit)
            {
                SetScafandr();
            }
            else
            {
                SetSelenit();
            }
        }
    }

    void SetSelenit()
    {
        IsSelenit = true;
        playerAn = selenit.GetComponent<Animator>();
        speed = runSpeed;
        selenit.SetActive(true);
        selenit.transform.GetChild(0).gameObject.SetActive(false); //Helmet
        scafandr.SetActive(false);
    }

    void SetScafandr()
    {
        if (playerObjects.Helmet || playerObjects.Scafandr)
        {
            IsSelenit = false;
            if (playerObjects.Scafandr)
            {
                scafandr.SetActive(true);
                selenit.SetActive(false);
                playerAn = scafandr.GetComponent<Animator>();
                speed = walkSpeed;

                if (playerObjects.Helmet)
                {
                    scafandr.transform.GetChild(0).gameObject.SetActive(false); //Selenit head
                    scafandr.transform.GetChild(1).gameObject.SetActive(true); //Helmet
                }
                else
                {
                    scafandr.transform.GetChild(0).gameObject.SetActive(true); //Selenit head
                    scafandr.transform.GetChild(1).gameObject.SetActive(false); //Helmet
                }
            }
            else //Helmet only
            {
                scafandr.SetActive(false);
                selenit.SetActive(true);
                selenit.transform.GetChild(0).gameObject.SetActive(true); //Helmet
                playerAn = selenit.GetComponent<Animator>();
                speed = runSpeed;

            }
        }
        
    }
}
