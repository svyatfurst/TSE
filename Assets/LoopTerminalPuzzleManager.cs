using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class LoopTerminalPuzzleManager : MonoBehaviour
{
    [Header("Drop Zones")]
    public CodeDropArea valueZone;
    public CodeDropArea semicolonZone;
    public CodeDropArea changeDirectionZone;

    [Header("UI & Buttons")]
    public GameObject terminalUIPanel;
    public TextMeshProUGUI terminalOutputText;

    public CharacterMovement characterMovement;

    public void OnRunClicked()
    {
        // ERROR CHECK: Are slots empty?
        if (valueZone.dockedCard == null || semicolonZone.dockedCard == null || changeDirectionZone.dockedCard == null)
        {
            ShowMessage("> ERROR: Missing parameters. Fill all brackets.", Color.red);
            return;
        }

        bool isValueCorrect = valueZone.dockedCard.cardData.cardTrait == CardTrait.Operator;
        bool isSemicolonCorrect = semicolonZone.dockedCard.cardData.cardTrait == CardTrait.JumpCommand;
        bool isChangeDirectionCorrect = changeDirectionZone.dockedCard.cardData.cardTrait == CardTrait.ChangeDirectionCommand;

        // ERROR CHECK: Are they the wrong cards?
        /*if (!isValueCorrect || !isSemicolonCorrect)
        {
            ShowMessage("> SYNTAX ERROR: Invalid arguments detected. Ejecting...", Color.red);
            ReturnToHand(valueZone);
            ReturnToHand(semicolonZone);
            return;
        }*/

        if (!isValueCorrect)
        {
            CloseTerminal();
            characterMovement.Scenario0();
            return;
        } else if(!isSemicolonCorrect) {
            CloseTerminal();
            characterMovement.Scenario1();
            return;
        } else if(!isChangeDirectionCorrect)
        {
            CloseTerminal();
            characterMovement.Scenario2();
            return;
        }
        else
        {
            CloseTerminal();
            characterMovement.Scenario3();
            return;
        }
        
        // SUCCESS!
        ShowMessage("> ACCESS GRANTED. Running the script...", Color.green);

        

        Destroy(valueZone.dockedCard.gameObject);
        Destroy(semicolonZone.dockedCard.gameObject);
        Destroy(changeDirectionZone.dockedCard.gameObject);

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
            zone.dockedCard.gameObject.SetActive(true);
            hand.cardsInHand.Add(zone.dockedCard);
            hand.ArrangeHand();

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
        ReturnToHand(changeDirectionZone);

        if (terminalOutputText != null) terminalOutputText.text = "";

        if (terminalUIPanel != null) terminalUIPanel.SetActive(false);
    }
}