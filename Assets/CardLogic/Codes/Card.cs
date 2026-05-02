
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [Header("Card Properties")]
    public CardData cardData;

    [Header("Visual Components")]
    public SpriteRenderer iconRenderer;
    public TextMeshPro textRenderer;

    private LineRenderer lineRenderer;
    private Vector3 startDragPosition;
    private bool isTargeting = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        // 1. FORCE THE SORTING LAYERS TO THE FRONT 
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        if (myRenderer != null)
        {
            myRenderer.sortingOrder = 100;
        }

        // 2. APPLY THE VISUALS FROM THE CARD DATA
        if (cardData != null)
        {
            if (cardData.useTextInsteadOfIcon)
            {
                // IT IS A TEXT CARD (False, Semicolon)
                if (textRenderer != null)
                {
                    textRenderer.text = cardData.cardText;
                    textRenderer.sortingOrder = 101; // Force text in front of card
                    textRenderer.gameObject.SetActive(true); // Turn text ON
                }
                if (iconRenderer != null)
                {
                    iconRenderer.gameObject.SetActive(false); // Turn picture OFF
                }
            }
            else
            {
                // IT IS A PICTURE CARD (Wire)
                if (iconRenderer != null)
                {
                    iconRenderer.sprite = cardData.cardIcon; // Swap Square for Wire
                    iconRenderer.sortingOrder = 101; // Force picture in front of card
                    iconRenderer.gameObject.SetActive(true); // Turn picture ON
                }
                if (textRenderer != null)
                {
                    textRenderer.gameObject.SetActive(false); // Turn text OFF
                }
            }
        }
    }

    private void OnMouseDown() // When the player clicks on the card, we start targeting.
    {
        isTargeting = true;
        startDragPosition = transform.position;
        lineRenderer.positionCount = 2;
    }

    private void OnMouseDrag() // While the player is dragging, we update the line to show the targeting.
    {
        if (isTargeting)
        {
            Vector3 mousePos = GetMousePositionInWorldSpace();
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, mousePos);
        }
    }

    /* private void OnMouseUp() // When the player releases the mouse button, we check if they dropped on a valid target.
     {
         isTargeting = false;
         lineRenderer.positionCount = 0;

         Vector3 mousePos = GetMousePositionInWorldSpace();

         Collider2D hitCollider = Physics2D.OverlapPoint((Vector2)mousePos);

         if (hitCollider != null && hitCollider.TryGetComponent(out ICardDropArea cardDropArea))
         {
             // We ask the drop area: "Do you accept this specific card?"
             bool wasAccepted = cardDropArea.OnCardDropped(this);

             if (wasAccepted)
             {
                 // The target accepted the card! Remove it from the hand.
                 HandManager hand = FindObjectOfType<HandManager>();
                 if (hand != null)
                 {
                     hand.RemoveCard(this);
                 }
             }
             else
             {
                 // The target rejected the card (e.g., we dropped a Code card on a Broken Wire).
                 SnapBackToHand();
             }
         }
         else
         {
             // We missed completely.
             SnapBackToHand();
         }
     }*/

    private void OnMouseUp()
    {
        isTargeting = false;
        lineRenderer.positionCount = 0;

        Vector3 mousePos = GetMousePositionInWorldSpace();

        // THE FIX: Check ALL colliders under the mouse, not just the top one!
        Collider2D[] hitColliders = Physics2D.OverlapPointAll((Vector2)mousePos);
        bool foundValidTarget = false;

        foreach (Collider2D hit in hitColliders)
        {
            // If we find an object with a drop area script...
            if (hit.TryGetComponent(out ICardDropArea cardDropArea))
            {
                // Ask the drop area if it accepts this card
                bool wasAccepted = cardDropArea.OnCardDropped(this);

                if (wasAccepted)
                {
                    // Success! Remove from hand.
                    HandManager hand = FindObjectOfType<HandManager>();
                    if (hand != null)
                    {
                        hand.RemoveCard(this);
                    }
                }
                else
                {
                    // It rejected the card (wrong type)
                    SnapBackToHand();
                }

                foundValidTarget = true;
                break; // Stop checking the rest of the overlapping colliders
            }
        }

        // If we looped through everything and never found a drop area...
        if (!foundValidTarget)
        {
            SnapBackToHand();
        }
    }

    private void SnapBackToHand() // If the card is not accepted by a target, it snaps back to its original position.
    {
        transform.position = startDragPosition;
    }

    private Vector3 GetMousePositionInWorldSpace() // Converts the mouse position to world space coordinates.
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0;
        return p;
    }
}