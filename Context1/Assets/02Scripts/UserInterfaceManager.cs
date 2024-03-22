using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;

    private void Awake()
    {
        instance = this; 
    }

    [SerializeField] private Abilities abilities;
    [System.Serializable]
    public class Abilities
    {
        [Header("Base Sprites")]
        public Image[] playerIcons;

        [Header("Artist Ability Sprites")]
        public Sprite biohorrorIcon;
        public Sprite playgroundIcon;

        [Header("Designer Ability Sprites")]
        public Sprite attackIcon;
        public Sprite jumpIcon;

        [Header("Developer Ability Sprites")]
        public Sprite gravityDownIcon;
        public Sprite gravityLeftIcon;
        public Sprite gravityUpIcon;
        public Sprite gravityRightIcon;
    }


    [Header("Buttons")]
    [SerializeField] private TextMeshProUGUI textButtons;
    private int currentButtons = 0;
    private int maxButtons = 3;

    // Call dit vanuit button manager wanneer je button pressed
    // (omdat max in principe altijd 3 is kan je er ook voor kiezen om deze weg te laten hier, ik heb hem gewoon toegevoegd om netter te maken
    public void UpdateButtonCount(int current, int max)
    {
        currentButtons = current;
        maxButtons = max;

        UpdateUI();
    }

    /// <summary>
    /// Dit is de functie die alle UI handelt.
    /// Call deze functie op het einde van elke functie die data aanpast (zoals UpdateButtonCount()
    /// Call deze functie A.U.B. niet vanuit update functie;
    ///     Deze functie hoeft alleen aangeroepen te worden als er iets verandert, dus niet 100x per seconde
    /// </summary>
    public void UpdateUI()
    {
        try
        {
            #region setup
            GameManager manager = GameManager.instance; // make a local instance to save performance
            PlayerSystem[] players = new PlayerSystem[3];
            GameObject[] playerGOs = GameObject.FindGameObjectsWithTag("Player");

            for(int i = 0; i < playerGOs.Length; i++)
            {
                players[i] = playerGOs[i].GetComponent<PlayerSystem>();
            }
            #endregion

            #region abilityUI
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i] == null) continue;

                switch (players[i].GetRole())
                {
                    case PlayerRole.Artist:
                        if (SceneSwitchDebug.instance.currentlyPlayground)
                            abilities.playerIcons[i].sprite = abilities.playgroundIcon;
                        else
                            abilities.playerIcons[i].sprite = abilities.biohorrorIcon;
                        break;
                }
            }

            #endregion

            #region text
            textButtons.text = currentButtons + " / " + maxButtons;
            #endregion
        }
        catch (NullReferenceException e) // De errors die je krijgt als je vergeten bent een waarde te geven aan iets
        {
            Debug.LogWarning("You forgot to assign a value to a variable...");
            Debug.LogException(e);
        }
        catch (Exception e) // Dit catcht alles wat niet te maken heeft met variables die je niet doorgeeft
        {
            Debug.LogException(e);
        }
    }
}
