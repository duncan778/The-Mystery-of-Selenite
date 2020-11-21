using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public GameObject selenit;
    public GameObject scafandr;
    [SerializeField] float walkSpeed = 3.75f;
    [SerializeField] float runSpeed = 7.5f;


    void Awake()
    {
        SetSelenit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GetComponent<PlayerMovingCC>().PlayerAvatar == selenit)
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
        GetComponent<PlayerMovingCC>().PlayerAvatar = selenit;
        GetComponent<PlayerJumpingCC>().PlayerAvatar = selenit;
        GetComponent<PlayerMovingCC>().Speed = runSpeed;
        selenit.SetActive(true);
        scafandr.SetActive(false);
    }

    void SetScafandr()
    {
        GetComponent<PlayerMovingCC>().PlayerAvatar = scafandr;
        GetComponent<PlayerJumpingCC>().PlayerAvatar = scafandr;
        GetComponent<PlayerMovingCC>().Speed = walkSpeed;
        scafandr.SetActive(true);
        selenit.SetActive(false);
    }
}
