// Hand Manager.cs
// script for card management. arranging hand and deck.
// By harry vowles
// 29339644
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<Card> cardsInHand = new List<Card>();

    [Header("Hand Settings")]
    public float cardSpacing = 1.5f;
    public float bottomOffset = 1.5f;

    // organise deck at bottom of screen
    public void ArrangeHand()
    {
        // 1. Clean up the list
        cardsInHand.RemoveAll(card => card == null);
        if (cardsInHand.Count == 0) return;

        // 2. Calculate positions
        Vector3 screenBottomCenter = new Vector3(Screen.width / 2f, 0, Mathf.Abs(Camera.main.transform.position.z));
        Vector3 worldBottomCenter = Camera.main.ScreenToWorldPoint(screenBottomCenter);

        worldBottomCenter.y += bottomOffset;
        worldBottomCenter.z = 0;

        float totalWidth = (cardsInHand.Count - 1) * cardSpacing;
        float startX = worldBottomCenter.x - (totalWidth / 2f);

        // 3. The Movement Loop
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            Vector3 targetPos = new Vector3(startX + (i * cardSpacing), worldBottomCenter.y, 0);

            // Get the RectTransform for UI Hitbox syncing
            RectTransform cardRect = cardsInHand[i].GetComponent<RectTransform>();

            if (cardRect != null)
            {
                cardRect.position = targetPos;

                // tell it to keep its own proportions
                // but ensure it isn't zero/tiny.
                if (cardRect.localScale.x < 0.1f)
                {
                    cardRect.localScale = new Vector3(1f, 1.5f, 1f); // specific rectangle ratio 
                }
            }
            else
            {
                // Fallback for standard sprites
                cardsInHand[i].transform.position = targetPos;
                cardsInHand[i].transform.localScale = Vector3.one;
            }
        } // End of For Loop

        Physics2D.SyncTransforms();
    } 

    public void RemoveCard(Card card)
    {
        if (cardsInHand.Contains(card))
        {
            cardsInHand.Remove(card);
            ArrangeHand();
        }
    }
} // End of Class