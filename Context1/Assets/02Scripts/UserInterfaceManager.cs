using System;
using UnityEngine;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;

    private void Awake()
    {
        instance = this; 
    }

    // buttons
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
    private void UpdateUI()
    {
        try
        {
            textButtons.text = currentButtons + " / " + maxButtons;
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
