// TerminalPuzzleManager.cs
// Script for terminal puzzle logic in room1
// By Harry Vowles
// 29339644

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TerminalPuzzleManager : MonoBehaviour
{
    [Header("Drop Zones")]
    public CodeDropArea valueZone;
    public CodeDropArea semicolonZone;

    [Header("UI & Buttons")]
    public GameObject terminalUIPanel;
    public TextMeshProUGUI terminalOutputText;

    [Header("Victory Action")]
    [Tooltip("The physical closed door blocking the path")]
    public GameObject closedDoor;
    [Tooltip("The background sprite showing the door is open")]
    public GameObject openedDoor;

    public void OnRunClicked()
    {
        // ERROR CHECK: Are slots empty?
        if (valueZone.dockedCard == null || semicolonZone.dockedCard == null)
        {
            ShowMessage("> ERROR: Missing parameters. Fill all brackets.", Color.red);
            return;
        }

        bool isValueCorrect = valueZone.dockedCard.cardData.cardTrait == CardTrait.BooleanValue;
        bool isSemicolonCorrect = semicolonZone.dockedCard.cardData.cardTrait == CardTrait.Syntax;

        // ERROR CHECK: Are they the wrong cards?
        if (!isValueCorrect || !isSemicolonCorrect)
        {
            ShowMessage("> SYNTAX ERROR: Invalid arguments detected. Ejecting...", Color.red);
            ReturnToHand(valueZone);
            ReturnToHand(semicolonZone);
            return;
        }

        // SUCCESS!
        ShowMessage("> ACCESS GRANTED. Opening door...", Color.green);

        Destroy(valueZone.dockedCard.gameObject);
        Destroy(semicolonZone.dockedCard.gameObject);

        valueZone.dockedCard = null;
        semicolonZone.dockedCard = null;

        // Door logic
        // 1. Delete the closed door and its colliders/messages
        /*if (closedDoor != null) Destroy(closedDoor);*/

        // 2. Turn on the visual open door background
        if (closedDoor != null) closedDoor.SetActive(false);

        StartCoroutine(CloseAfterDelay(2f));
    }

    private void ShowMessage(string msg, Color color)
    {
        if (terminalOutputText != null)
        {
            terminalOutputText.text = msg;
            terminalOutputText.color = color;
        }
    }

    private void ReturnToHand(CodeDropArea zone)
    {
        if (zone.dockedCard != null)
        {
            HandManager hand = FindObjectOfType<HandManager>();

            if (hand != null)
            {
              
                zone.dockedCard.transform.SetParent(hand.transform, false);

                // Ensure it is visible and at full scale
                zone.dockedCard.gameObject.SetActive(true);
                zone.dockedCard.transform.localScale = Vector3.one;

                // Add back to logic and arrange
                if (!hand.cardsInHand.Contains(zone.dockedCard))
                {
                    hand.cardsInHand.Add(zone.dockedCard);
                }
                hand.ArrangeHand();
            }

            // Clear the zone so it's ready for a new card
            zone.dockedCard = null;
            if (zone.gapText != null)
            {
                zone.gapText.text = zone.defaultText;
                zone.gapText.color = Color.white;
            }
        }
    }


    public void CloseTerminal()
    {
        Time.timeScale = 1f;

        ReturnToHand(valueZone);
        ReturnToHand(semicolonZone);

        if (terminalOutputText != null) terminalOutputText.text = "";

        if (terminalUIPanel != null) terminalUIPanel.SetActive(false);
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        CloseTerminal();
    }
}