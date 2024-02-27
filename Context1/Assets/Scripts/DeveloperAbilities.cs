using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class DeveloperAbilities : MonoBehaviour
{

    int gravityDirection = 0;
    float gravity = 9.81f;
    float rot = 0;
    float abilityCooldown = 2f;
    float coolDownTimer = 0f;
    bool coolDownReset = true;
    int abilitiesCaseNr = 0;


    public enum DevAbilitiesEnum
    {
        gravitySwap, adjustJump, adjustSpeed
    }
    public DevAbilitiesEnum Abilities;
    private void CheckAbilityState()
    {
        switch (Abilities)     // AbilityState Behaviour
        {
            case DevAbilitiesEnum.gravitySwap: SwapGravity(); break;
            case DevAbilitiesEnum.adjustJump: JumpAdjust(); break;
            case DevAbilitiesEnum.adjustSpeed: SpeedAdjust(); break;
        }

        switch (abilitiesCaseNr)// Cycle through states.
        {
            case 0: Abilities = AbilitiesEnum.gravitySwap; break;
            case 1: Abilities = AbilitiesEnum.adjustJump; break;
            case 2: Abilities = AbilitiesEnum.adjustSpeed; break;

        }
        abilitiesCaseNr %= 3;
    }


    void Update()
    {
        Debug.Log(coolDownTimer);

        CheckAbilityState();   // activates state every frame

        if(coolDownReset == false)  // If cooldown is active, activate timer
        {
            CountCoolDownTime();
        }
        if (Input.GetKeyDown(KeyCode.Q)) abilitiesCaseNr++; // Cycle to next stage.
    }


    public void SwapAbility()
    {
        
    }

    // Different Gravity directions
        Vector3    up = new Vector3(0,   9.81f, 0);
        Vector3 right = new Vector3(9.81f,  0,    0);
        Vector3  down = new Vector3(0,  -9.81f, 0);
        Vector3  left = new Vector3(-9.81f, 0,    0);
    public void SwapGravity()
    {

        //float gravity;
        //Vector3 test =new Vector3(0, -1.0f, 0)

        switch(gravityDirection)
        {
            case 0: Physics.gravity = down; break;
            case 1: Physics.gravity = left; break;
            case 2: Physics.gravity = up; break;
            case 3: Physics.gravity = right; break;
        }

        if (Input.GetKeyDown(KeyCode.E) && coolDownReset == true)    // if ability is triggered with E
        {
            coolDownReset = false;

            gravityDirection++; // Swap to 90 degrees further direction

            rot -= 90;  // Rotate value -= 90 degrees
            gameObject.transform.rotation = Quaternion.Euler(0, 0, rot);    // apply rotation to player

        }

        gravityDirection %= 4;
    }


    public void JumpAdjust()
    {
        if (Input.GetKeyDown(KeyCode.E) && coolDownReset == true)
        {
            coolDownReset = false;
            GameManager.instance.SwapJump();
        }
    }


    public void SpeedAdjust()
    {
        if (Input.GetKeyDown(KeyCode.E) && coolDownReset == true)
        {
            coolDownReset = false;
            GameManager.instance.SwapSpeed();
        }
    }


    void CountCoolDownTime()
    {
        coolDownTimer+= Time.deltaTime;

        if(coolDownTimer > abilityCooldown) {
            coolDownTimer = 0;
            coolDownReset = true;
            return;
        }
    }
}
