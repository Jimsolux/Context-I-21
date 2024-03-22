using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<AstarPath> pathfinders = new();

    [Header("Player variables")]
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float playerSpeed;         // ACTUAL
    public float jumpHeight1;
    public float jumpHeight2;
    public float jumpHeight;    // ACTUAL

    public PlayerAction activeAction;

    public DevAbilitiesEnum devAbilities;
    public DesAbilitiesEnum desAbilities;
    public ArtAbilitiesEnum artAbilities;
    public Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
    public bool sidewaysControls = false;

    [SerializeField] private SceneSwitchDebug sceneSwitcher;
    //Monster variables


    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length > 0)
            Camera.main.gameObject.SetActive(false);

        foreach (GameObject g in players)
        {
            g.GetComponent<PlayerSystem>().Die();
        }

        playerSpeed = walkSpeed;
        jumpHeight = jumpHeight1;

        GameObject[] pathfinderObjects = GameObject.FindGameObjectsWithTag("Pathfinding");

        foreach (GameObject g in pathfinderObjects)
        {
            pathfinders.Add(g.GetComponent<AstarPath>());
        }
        UserInterfaceManager.instance.UpdateUI();
    }

    public void UpdatePathfinding()
    {
        foreach (AstarPath p in pathfinders)
        {
            p.Scan();
        }
    }

    private void Update()
    {
    }

    public void UseAbility(PlayerRole role)
    {
        switch (role)
        {
            case PlayerRole.Artist:
                SwapMood();
                break;
            case PlayerRole.Developer:
                switch (devAbilities)
                {
                    case DevAbilitiesEnum.GravitySwap:
                        GravitySwap();
                        break;
                        //case DevAbilitiesEnum.AdjustJump: AdjustJump();
                        //    break;
                        //case DevAbilitiesEnum.AdjustSpeed: AdjustSpeed();
                        //    break;
                }
                UserInterfaceManager.instance.UpdateUI();
                break;
            case PlayerRole.Designer:
                switch (desAbilities)
                {
                    case DesAbilitiesEnum.Jump:
                        desAbilities = DesAbilitiesEnum.Attack;
                        break;
                    case DesAbilitiesEnum.Attack:
                        desAbilities = DesAbilitiesEnum.Jump;
                        ;
                        break;
                }
                UserInterfaceManager.instance.UpdateUI();
                break;

        }
    }

    public void ChangeAbility(PlayerRole role, int swapDir)
    {
        /*
        switch (role)
        {
            case PlayerRole.Developer:
                index = (int)devAbilities + swapDir;

                if (index >= Enum.GetValues(typeof(DevAbilitiesEnum)).Length) index = 0;    //Max Index reset
                if (index < 0) index = Enum.GetValues(typeof(DevAbilitiesEnum)).Length - 1; //Min Index reset
                devAbilities = (DevAbilitiesEnum)index; // Sets the active devAbility to the index.
                break;

            case PlayerRole.Designer:
                index = (int)desAbilities + swapDir;

                if (index >= Enum.GetValues(typeof(DesAbilitiesEnum)).Length) index = 0;    //Max Index reset
                if (index < 0) index = Enum.GetValues(typeof(DesAbilitiesEnum)).Length - 1; //Min Index reset
                desAbilities = (DesAbilitiesEnum)index;// Sets the active desAbility to the index.
                break;

            case PlayerRole.Artist:
                index = (int)artAbilities + swapDir;

                if (index >= Enum.GetValues(typeof(ArtAbilitiesEnum)).Length) index = 0;    //Max Index reset
                if (index < 0) index = Enum.GetValues(typeof(ArtAbilitiesEnum)).Length - 1; //Min Index reset
                artAbilities = (ArtAbilitiesEnum)index;// Sets the active desAbility to the index.
                break;
        }*/
    }


    #region dev abilities
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
            case GravityDirectionEnum.Down:
                Physics.gravity = down;
                targetRotation = Quaternion.Euler(0, 0, 0);
                sidewaysControls = false;
                break;
            case GravityDirectionEnum.Left:
                Physics.gravity = left;
                targetRotation = Quaternion.Euler(0, 0, -90);
                sidewaysControls = true;

                break;
            case GravityDirectionEnum.Up:
                Physics.gravity = up;
                targetRotation = Quaternion.Euler(0, 0, 180);
                sidewaysControls = false;

                break;
            case GravityDirectionEnum.Right:
                Physics.gravity = right;
                targetRotation = Quaternion.Euler(0, 0, 90);
                sidewaysControls = true;

                break;
        }
    }

    private bool jumpHeightBool = true;
    public void AdjustJump() // Change players jumpheight
    {
        if (jumpHeightBool) jumpHeight = jumpHeight2;
        if (!jumpHeightBool) jumpHeight = jumpHeight1;

        jumpHeightBool = !jumpHeightBool;
    }

    private bool runSpeedBool = true;
    public void AdjustSpeed() // Change players speed
    {
        if (runSpeedBool) playerSpeed = runSpeed;
        if (!runSpeedBool) playerSpeed = walkSpeed;

        runSpeedBool = !runSpeedBool;
    }

    #endregion

    #region des abilities

    public void Attack()
    {

    }


    #endregion

    #region artAbility

    public void SwapMood()
    {
        sceneSwitcher.SwapMood();
    }

    #endregion
}
