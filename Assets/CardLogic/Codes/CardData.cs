using UnityEngine;

public enum CardTrait
{
    None,
    Wire,
    BooleanValue,
    Syntax
}

[CreateAssetMenu(fileName = "NewCardData", menuName = "Card System/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public CardTrait cardTrait;

    [Header("Visual Type")]
    [Tooltip("Check this if the card should show text instead of an image")]
    public bool useTextInsteadOfIcon;

    [Header("Visuals (Fill out one)")]
    public Sprite cardIcon; // Put the wire picture here
    public string cardText; // Put "false" or ";" here
}