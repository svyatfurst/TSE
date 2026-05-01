using UnityEngine;
using TMPro; // Needed to talk to your UI Text


public class CodeDropArea : MonoBehaviour, ICardDropArea
{
    [Header("Puzzle Settings")]
    [Tooltip("Type the exact name of the ScriptableObject for the 'False' card here")]
    public string correctCardName = "FalseCard";

    [Header("UI Visuals")]
    [Tooltip("Drag the TextMeshPro object that has your semicolon ';' into here")]
    public TextMeshProUGUI gapText;

    // This is automatically called by your Card.cs script when the player releases the mouse over this object!
    public bool OnCardDropped(Card card)
    {
        // 1. Check if the card has data, and if its name matches the winning card
        // (You might need to change '.name' to whatever variable is inside your CardData script, like '.cardTitle')
        if (card.cardData != null && card.cardData.name == correctCardName)
        {
            Debug.Log("HACK SUCCESSFUL! Code Fixed!");

            // 2. Make the UI look like the code was typed in
            if (gapText != null)
            {
                // Replace the blank space/semicolon with the actual code
                gapText.text = "false;";

                // Color it that specific hacker blue using Unity's Color32
                gapText.color = new Color32(86, 156, 214, 255);
            }

            // TODO: Trigger your 2D Door script to open here!

            // 3. Return true so Card.cs knows to destroy the card and re-center the hand
            return true;
        }
        else
        {
            Debug.Log("Wrong code injected! The system rejects it.");

            // Return false so Card.cs script snaps the targeting line back
            return false;
        }
    }
}