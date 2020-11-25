using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool IsGameOver { get; set; }
    protected static Animator playerAn;
    protected static CharacterController playerCC;
    protected static float speed;
    protected static bool isOnGround;

    private void Awake()
    {
        playerCC = GetComponent<CharacterController>();
    }

}
