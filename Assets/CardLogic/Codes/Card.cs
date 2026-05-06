// Card.cs
// Used to handle card drag and drop logic and visual scaling of text/image.
// by Harry Vowles
// 29339644
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Card Class
public class Card : MonoBehaviour
{
    [Header("Card Properties")]
    public CardData cardData; // type of card

    [Header("Visual Components")] //icon or text
    public SpriteRenderer iconRenderer; 
    public TextMeshPro textRenderer;

    // line rendering
    private LineRenderer lineRenderer;
    private Vector3 startDragPosition;
    private bool isTargeting = false;
    private Vector3 originalScale;

    void Start()
    {
        
        originalScale = transform.localScale; // saves scale of cards so doesn't adjust later
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        // forces sorting layers to front 
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        if (myRenderer != null)
        {
            myRenderer.sortingOrder = 100;
        }

        // apply card data visuakls to card
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

    private void OnMouseDown() // When the player clicks on the card
    {
        isTargeting = true;
        startDragPosition = transform.position;
        lineRenderer.positionCount = 2;
    }

    private void OnMouseDrag()
    {
        if (isTargeting)
        {
            Vector3 mousePos = GetMousePositionInWorldSpace();

            // Use the CURRENT position so the line follows the card if it shifts
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, mousePos);
        }
    }

    private void OnMouseUp()
    {
        isTargeting = false;
        lineRenderer.positionCount = 0;

        Vector3 mousePos = GetMousePositionInWorldSpace();

        // Check all colliders under Mouse
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
                    // Success: Remove from hand list.
                    HandManager hand = FindObjectOfType<HandManager>();
                    if (hand != null)
                    {
                        hand.RemoveCard(this);
                    }

                    // Move the card to the drop area 
                    transform.SetParent(hit.transform, false);

                    // Center the card exactly on the drop slot
                    transform.position = hit.transform.position;
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
        // 1. Move it back to the position saved at start of drag
        transform.position = startDragPosition;

        // 2. Scale to original scale saved in beginning of code.
        transform.localScale = originalScale;
    }

    private Vector3 GetMousePositionInWorldSpace() // Converts the mouse position to world space coordinates.
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0;
        return p;
    }
}