/// <summary>
/// Class Card
/// Used for the card objects in the player's hand. Handles dragging to target and snapping back if the player misses.
/// By Harry Vowles (29339644)
/// </summary> 

using UnityEngine;

public class Card : MonoBehaviour
{
    private LineRenderer lineRenderer; // Used to draw the targeting line
    private Vector3 startDragPosition; // Remember where the card started so we can snap back if we miss
    private bool isTargeting = false; // True while the player is dragging the card to target something

    void Start() // Set up the LineRenderer component
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    // On Clicking the card, we start targeting. We remember where the card is so we can snap back if we miss.
    private void OnMouseDown()
    {
        isTargeting = true;
        startDragPosition = transform.position; // Remembers its spot in the Hand
        lineRenderer.positionCount = 2;
    }

    // While dragging, we update the LineRenderer to draw a line from the card to the mouse position.
    private void OnMouseDrag()
    {
        if (isTargeting)
        {
            Vector3 mousePos = GetMousePositionInWorldSpace();
            lineRenderer.SetPosition(0, transform.position); // Line starts at the card
            lineRenderer.SetPosition(1, mousePos);           // Line ends at the mouse
        }
    }

    // On releasing the mouse button, we check if we released over a valid target. If so, we trigger the card's effect. If not, we snap back to the hand.
    private void OnMouseUp()
    {
        isTargeting = false;
        lineRenderer.positionCount = 0;

        Vector3 mousePos = GetMousePositionInWorldSpace();

        // Shoots a point into the 2D world exactly where you let go of the mouse
        Collider2D hitCollider = Physics2D.OverlapPoint((Vector2)mousePos);

        if (hitCollider != null && hitCollider.TryGetComponent(out ICardDropArea cardDropArea))
        {
            // We hit a valid target ( Can be anything that implements ICardDropArea, like an enemy or the player).

            // 1. Tell the HandManager to remove this card so the hand closes the gap
            HandManager hand = FindObjectOfType<HandManager>();
            if (hand != null)
            {
                hand.RemoveCard(this);
            }

            // 2. Trigger the drop logic on the target
            cardDropArea.OnCardDropped(this);
        }
        else
        {
            // We missed. Snap the card back to its slot in the hand.
            transform.position = startDragPosition;
        }
    }

    private Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0;
        return p;
    }
}