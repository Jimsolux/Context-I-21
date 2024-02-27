using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerInputManager inputManager;
    private int activePlayers;

    [Header("Interface variables")]
    [SerializeField] private GameObject canvasMain;
    [SerializeField] private GameObject[] connectionScreens;

    [Header("Player variables")]
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float playerSpeed;         // ACTUAL
    public float jumpHeight1;
    public float jumpHeight2;
    public float jumpHeight;    // ACTUAL

    public PlayerAction activeAction;
    //Monster variables


    void Awake()
    {
        instance = this;
        try { canvasMain.SetActive(true); } // Only activated on game start to prevent a massive UI blob from appearing in editor view
        catch { Debug.LogWarning("No canvas found, please ensure there is always a canvas in the scene. The correct canvas can be found under Prefabs/Display Canvas"); }
    }
    private void Start()
    {
        playerSpeed = walkSpeed;
        jumpHeight = jumpHeight1;
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            GravitySwap();
        }
    }

    public void AddPlayer(PlayerSystem player)
    {
        try
        {
            // The - 1 is due to computer language mumbo jumbo.
            int activePlayers = inputManager.playerCount - 1;

            PlayerRole role = (PlayerRole)activePlayers;

            player.Setup(role, activePlayers);

            connectionScreens[activePlayers].SetActive(false);
        }
        catch {
            Debug.LogError("Player has not been instantiated with the input manager"); 
            Debug.LogError("Please do not add player objects to the scene, these get added with the PlayerInputComponent"); 
        }
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


    public GravityDirectionEnum gravityDirection;
    public void GravitySwap()
    {
        Vector3 up = new Vector3(0, 9.81f, 0);
        Vector3 right = new Vector3(9.81f, 0, 0);
        Vector3 down = new Vector3(0, -9.81f, 0);
        Vector3 left = new Vector3(-9.81f, 0, 0);

        int gravityDirectionInt = (int)gravityDirection + 1;

        if (gravityDirectionInt >= 4)
            gravityDirectionInt = 0;

        gravityDirection = (GravityDirectionEnum)gravityDirectionInt;

        switch (gravityDirection)
        {
            case GravityDirectionEnum.Down: Physics.gravity = down; break;
            case GravityDirectionEnum.Left: Physics.gravity = left; break;
            case GravityDirectionEnum.Up: Physics.gravity = up; break;
            case GravityDirectionEnum.Right: Physics.gravity = right; break;
        }
    }
}
