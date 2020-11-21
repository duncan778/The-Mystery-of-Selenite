using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : PlayerManager
{
    public GameObject selenit;
    public GameObject scafandr;
    [SerializeField] float walkSpeed = 3.75f;
    [SerializeField] float runSpeed = 7.5f;
    bool isSelenit;

    void Awake()
    {
        SetSelenit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isSelenit)
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
        isSelenit = true;
        playerAn = selenit.GetComponent<Animator>();
        speed = runSpeed;
        selenit.SetActive(true);
        scafandr.SetActive(false);
    }

    void SetScafandr()
    {
        isSelenit = false;
        playerAn = scafandr.GetComponent<Animator>();
        speed = walkSpeed;
        scafandr.SetActive(true);
        selenit.SetActive(false);
    }
}
