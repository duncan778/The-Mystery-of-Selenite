using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public GameObject selenit;
    public GameObject scafandr;
    [SerializeField] float walkSpeed = 3.75f;
    [SerializeField] float runSpeed = 7.5f;


    void Start()
    {
        GetComponent<PlayerMovingCC>().PlayerAvatar = selenit;
        GetComponent<PlayerJumpingCC>().PlayerAvatar = selenit;
        GetComponent<PlayerMovingCC>().WalkSpeed = runSpeed;
        scafandr.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GetComponent<PlayerMovingCC>().PlayerAvatar == selenit)
            {
                GetComponent<PlayerMovingCC>().PlayerAvatar = scafandr;
                GetComponent<PlayerJumpingCC>().PlayerAvatar = scafandr;
                GetComponent<PlayerMovingCC>().WalkSpeed = walkSpeed;
                scafandr.SetActive(true);
                selenit.SetActive(false);
            }
            else
            {
                GetComponent<PlayerMovingCC>().PlayerAvatar = selenit;
                GetComponent<PlayerJumpingCC>().PlayerAvatar = selenit;
                GetComponent<PlayerMovingCC>().WalkSpeed = runSpeed;
                selenit.SetActive(true);
                scafandr.SetActive(false);
            }
        }
    }
}
