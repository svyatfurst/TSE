using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDropRed : MonoBehaviour, ICardDropArea
{
    public void OnCardDropped(Card card)
    {
        card.transform.position = transform.position; // Move the card to the position of the drop area (e.g., a slot)
        Debug.Log("Card dropped on red area!"); // Log a message to indicate that the card was successfully dropped on the red area
    }
}
