/// <summary>
/// class HandManager
/// Class to manage the player's hand of cards, keeping them centered at the bottom of the screen.
/// Created by: Harry Vowles (29339644)
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<Card> cardsInHand = new List<Card>();  // List to hold the cards currently in the player's hand

    [Header("Hand Settings")] 
    public float cardSpacing = 1.5f; // Gap between cards
    public float bottomOffset = 1.5f; // How high off the bottom edge

    
    void Start() 
    {
        ArrangeHand(); // Initial arrangement of the hand at the start of the game
    }

    // Call this whenever a card is drawn or played to re-center everything
    public void ArrangeHand()
    {
        // Clean up any empty slots just in case a card was destroyed
        cardsInHand.RemoveAll(card => card == null);

        if (cardsInHand.Count == 0) return;

        // 1. Find the bottom center of the camera in World Space
        Vector3 screenBottomCenter = new Vector3(Screen.width / 2f, 0, Mathf.Abs(Camera.main.transform.position.z));
        Vector3 worldBottomCenter = Camera.main.ScreenToWorldPoint(screenBottomCenter);

        worldBottomCenter.y += bottomOffset; // Move it up slightly so it's visible
        worldBottomCenter.z = 0;

        // 2. Calculate the total width of the hand to center it
        float totalWidth = (cardsInHand.Count - 1) * cardSpacing;
        float startX = worldBottomCenter.x - (totalWidth / 2f);

        // 3. Move the cards into position
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            Vector3 targetPos = new Vector3(startX + (i * cardSpacing), worldBottomCenter.y, 0);
            cardsInHand[i].transform.position = targetPos;
        }
    }

    public void RemoveCard(Card card)
    {
        cardsInHand.Remove(card);
        ArrangeHand(); // Re-center the remaining cards instantly!
    }
}