using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartMenu : MonoBehaviour
{
    public static StartMenu instance;

    [SerializeField] private PlayerInputManager inputManager;

    [Header("Interface variables")]
    [SerializeField] private GameObject canvasMain;
    [SerializeField] private GameObject[] connectionScreens;

    private void Awake()
    {
        instance = this;
        try { canvasMain.SetActive(true); } // Only activated on game start to prevent a massive UI blob from appearing in editor view
        catch { Debug.LogWarning("No canvas found, please ensure there is always a canvas in the scene. The correct canvas can be found under Prefabs/Display Canvas"); }

        DontDestroyOnLoad(this);
    }
    public void AddPlayer(PlayerSystem player)
    {
        //try
        //{
        // The - 1 is due to computer language mumbo jumbo.
        int activePlayers = inputManager.playerCount - 1;

        PlayerRole role = (PlayerRole)activePlayers;

        player.Setup(role, activePlayers);

        connectionScreens[activePlayers].SetActive(false);


        //}
        /*catch (Exception e) {
            Debug.LogError("Player has not been instantiated with the input manager"); 
            Debug.LogError("Please do not add player objects to the scene, these get added with the PlayerInputComponent");
            Debug.LogException(e);
        }*/
    }
}
