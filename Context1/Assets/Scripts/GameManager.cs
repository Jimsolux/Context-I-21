using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Player variables
    public float walkSpeed;
    public float runSpeed;
    public float playerSpeed;         // ACTUAL
    public float jumpHeight1;
    public float jumpHeight2;
    public float jumpHeight;    // ACTUAL

    //Monster variables

    private void Start()
    {
        playerSpeed = walkSpeed;
        jumpHeight = jumpHeight1;
    }

    public void SwapSpeed() // Change players speed
    {
        if (playerSpeed == runSpeed) playerSpeed = walkSpeed;
        if (playerSpeed == walkSpeed) playerSpeed = runSpeed;
    }

    public void SwapJump() // Change players jumpheight
    {
        if (jumpHeight == jumpHeight1) jumpHeight = jumpHeight2;
        if (jumpHeight == jumpHeight2) jumpHeight = jumpHeight1;
    }
}
