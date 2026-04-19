/// <summary>
/// Class Card
/// Used for the card objects in the player's hand. Handles dragging to target and snapping back if the player misses.
/// By Harry Vowles (29339644)
/// </summary> 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Card Properties")]
    // This connects the card to the ScriptableObject so it knows if it's a Wire, Code, etc.
    public CardData cardData;

    private LineRenderer lineRenderer;
    private Vector3 startDragPosition;
    private bool isTargeting = false;

    void Start() // setup for LineRenderer
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
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

    private void OnMouseUp() // When the player releases the mouse button, we check if they dropped on a valid target.
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