///  <summary>
///  CardData class
///  This is a ScriptableObject that holds the data for each card type. It can be used to create different card types with different traits and effects.
///  By Harry Vowles (29339644)
///  </summary> 
using UnityEngine;

// Define all the possible types a card can be here
public enum CardTrait
{
    None,
    Wire,
}

[CreateAssetMenu(fileName = "NewCardData", menuName = "Card System/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public CardTrait cardTrait;
    // You can add more data here later, like:
    // public Sprite cardImage;
    // public string cardDescription;
}