/// <summary>
/// Broken Wire Class for puzzle logic
/// By Harry Vowles (29339644)
/// </summary>

using UnityEngine;

public class BrokenWire : MonoBehaviour, ICardDropArea
{
    [SerializeField] private CardTrait requiredTrait = CardTrait.Wire;

    public bool OnCardDropped(Card droppedCard)
    {
        // 1. Check if the card has data and matches the required trait
        if (droppedCard.cardData != null && droppedCard.cardData.cardTrait == requiredTrait)
        {
            Debug.Log("Success! The wire was fixed!");

            // 2. Do the fix logic here (change sprite, play sound, open door, etc.)

            // 3. Destroy the card object since it was used
            Destroy(droppedCard.gameObject);

            return true; // Tells the card it was accepted
        }
        else
        {
            Debug.Log("Wrong card! This needs a Wire card.");
            return false; // Tells the card to snap back
        }
    }
}