using System;
using UnityEngine;

///  <summary>
/// Dit script bevat de call-back naar alle variables in de GameManager.
/// Om dit script toe te passen gebruik je Variables.[functienaam]();, net zoals je andere functies op zal roepen.
/// Wanneer je een nieuwe variable aanmaakt in de GameManager, maak dan een nieuwe Get[naamVariable]() en Set[naamVariable]() aan.
///     Doe dit alsjeblieft op dezelfde methode die hier al geschreven staan, ik heb er ook comments bij gezet voor verduidelijking van wat alles doet.
///     Als je iets in deze code of de toepassing ervan niet begrijpt, DM mij (mitchel) dan even.
///     
/// Waarom op deze manier?
///     Zodat je vanuit alle scripts de variables van de GameManager kan oproepen en aanpassen.
///     Kan dat ook zonder dit script?; ja, maar liever niet i.v.m. netheid en leesbaarheid van scripts.
/// </summary>

public static class Variables
{
    /// <summary>
    /// Methode van aanmaak nieuwe functies;
    /// (Je kan gewoon letterlijk een bestaande functie copy/pasten en aanpassen)
    /// 
    /// Maak een region voor de variable.
    ///     Dit doe je door #region [variable naam] te typen boven de functies, en #endregion onder de functies.
    ///     Voorbeelden hiervoor staan al bij de bestaande functies.
    ///     
    /// Tussen elke functie: Gebruik witregels.
    /// Tussen elke region: Gebruik ook witregels.
    ///     Dit zorgt gewoon voor betere leesbaarheid in script.
    ///     
    /// Type; maak de type van de functie altijd gelijk aan de soort variable.
    ///     Dus; al wil je een Vector3 krijgen, verander je de functie type naar public static Vector3 [naam]().
    ///     Bij de Set functies pas je dan de type variable aan de tussen de haakjes staat.
    ///     
    /// - Naam; Get/Set + variable naam.
    /// In try{} pas je de variable naam aan naar de juist variable.
    /// In catch{} pas je de naam van de functie in de debug aan naar de juiste functie naam.
    ///     Dit voorkomt verwarring wanneer een bepaalde waarde aangeroepen wilt worden, zodat we gelijk weten welke variable raar doet.
    /// </summary>

    #region walkSpeed
    // Geeft de walkSpeed variable uit GameManager door. 
    public static float GetWalkSpeed()
    {
        try { return GameManager.instance.walkSpeed; } 
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > geef de variable door
        catch (Exception e) { Debug.LogWarning("Could not get variable from GetWalkSpeed(); " + e); } 
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
        return 0;
        // Al is GameManager.instance nog niet aangemaakt geef je een standaard waarde door, die is in het algemeen altijd 0.
    }

    // Past de walkSpeed variable in GameManager aan.
    public static void SetWalkSpeed(float value)
    {
        try { GameManager.instance.walkSpeed = value; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > verander variable
        catch (Exception e) { Debug.LogWarning("Could not set variable from SetWalkSpeed(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
    }
    #endregion

    #region runSpeed
    // Geeft de runSpeed variable uit GameManager door. 
    public static float GetRunSpeed()
    {
        try { return GameManager.instance.runSpeed; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > geef de variable door
        catch (Exception e) { Debug.LogWarning("Could not get variable from GetRunSpeed(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
        return 0;
        // Al is GameManager.instance nog niet aangemaakt geef je een standaard waarde door, die is in het algemeen altijd 0.
    }

    // Past de runSpeed variable in GameManager aan.
    public static void SetRunSpeed(float value)
    {
        try { GameManager.instance.runSpeed = value; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > verander variable
        catch (Exception e) { Debug.LogWarning("Could not set variable from SetRunSpeed(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
    }
    #endregion

    #region playerSpeed
    // Geeft de playerSpeed variable uit GameManager door. 
    public static float GetPlayerSpeed()
    {
        try { return GameManager.instance.playerSpeed; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > geef de variable door
        catch (Exception e) { Debug.LogWarning("Could not get variable from GetPlayerSpeed(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
        return 0;
        // Al is GameManager.instance nog niet aangemaakt geef je een standaard waarde door, die is in het algemeen altijd 0.
    }

    // Past de playerSpeed variable in GameManager aan.
    public static void SetPlayerSpeed(float value)
    {
        try { GameManager.instance.playerSpeed = value; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > verander variable
        catch (Exception e) { Debug.LogWarning("Could not set variable from SetPlayerSpeed(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
    }
    #endregion

    #region jumpHeight1
    // Geeft de jumpHeight1 variable uit GameManager door. 
    public static float GetJumpHeight1()
    {
        try { return GameManager.instance.jumpHeight1; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > geef de variable door
        catch (Exception e) { Debug.LogWarning("Could not get variable from JumpHeight1(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
        return 0;
        // Al is GameManager.instance nog niet aangemaakt geef je een standaard waarde door, die is in het algemeen altijd 0.
    }

    // Past de jumpHeight1 variable in GameManager aan.
    public static void SetJumpHeight1(float value)
    {
        try { GameManager.instance.jumpHeight1 = value; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > verander variable
        catch (Exception e) { Debug.LogWarning("Could not set variable from JumpHeight1(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
    }
    #endregion

    #region jumpHeight2
    // Geeft de jumpHeight2 variable uit GameManager door. 
    public static float GetJumpHeight2()
    {
        try { return GameManager.instance.jumpHeight2; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > geef de variable door
        catch (Exception e) { Debug.LogWarning("Could not get variable from JumpHeight2(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
        return 0;
        // Al is GameManager.instance nog niet aangemaakt geef je een standaard waarde door, die is in het algemeen altijd 0.
    }

    // Past de jumpHeight2 variable in GameManager aan.
    public static void SetJumpHeight2(float value)
    {
        try { GameManager.instance.jumpHeight2 = value; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > verander variable
        catch (Exception e) { Debug.LogWarning("Could not set variable from JumpHeight2(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
    }
    #endregion

    #region jumpHeight
    // Geeft de jumpHeight variable uit GameManager door. 
    public static float GetJumpHeight()
    {
        try { return GameManager.instance.jumpHeight; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > geef de variable door
        catch (Exception e) { Debug.LogWarning("Could not get variable from JumpHeight(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
        return 0;
        // Al is GameManager.instance nog niet aangemaakt geef je een standaard waarde door, die is in het algemeen altijd 0.
    }

    // Past de jumpHeight variable in GameManager aan.
    public static void SetJumpHeight(float value)
    {
        try { GameManager.instance.jumpHeight = value; }
        // Kijkt of GameManager.instance al aangemaakt is. Ja? > verander variable
        catch (Exception e) { Debug.LogWarning("Could not set variable from JumpHeight(); " + e); }
        // Is GameManager.instance niet aangemaakt? > print een warning
        // Dit zorgt dat je niet volgespamt wordt met error messages die code kunnen breken.
    }
    #endregion
}
