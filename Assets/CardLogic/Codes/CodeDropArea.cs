// CodeDropArea.cs
// logic for droppable card area 
// By Harry Vowles
// 29339644
using UnityEngine;
using TMPro;

public class CodeDropArea : MonoBehaviour, ICardDropArea
{
    [Header("Current State")]
    [Tooltip("This remembers which card is currently 'plugged in' to the terminal")]
    public Card dockedCard; 

    [Header("Visuals")]
    [Tooltip("Text object ")]
    public TextMeshProUGUI gapText;
    public string defaultText = "___";

    public bool OnCardDropped(Card card)
    {
        // If there is already a card plugged into this slot, reject the new one
        if (dockedCard != null) return false;

        if (card.cardData != null)
        {
            // card so the Manager's RUN button can check it 
            dockedCard = card;

            // Hide the physical 2D card object 
            card.gameObject.SetActive(false);

            // Update the terminal text to show what was plugged in
            if (gapText != null)
            {
                // If it's a text card, show the text. If it's an image card, show its name.
                gapText.text = card.cardData.useTextInsteadOfIcon ? card.cardData.cardText : card.cardData.cardName;

                // Change the text color to hacker blue so they know it's filled
                gapText.color = new Color32(86, 156, 214, 255);
            }

            return true; 
        }

        return false;
    }
}