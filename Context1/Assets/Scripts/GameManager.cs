using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public void AddPlayer(PlayerSystem player)
    {
        try
        {
            int activePlayers = inputManager.playerCount;

            PlayerRole role = (PlayerRole)activePlayers;

            player.Setup(role, activePlayers);

            // The - 1 is due to computer language mumbo jumbo, otherwise it takes wrong ID.
            connectionScreens[activePlayers - 1].SetActive(false);
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
}
