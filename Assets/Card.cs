using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Collider2D col; // The collider component of the card
    private Vector3 startDragPosition; // The position where the card was when the drag started

    void Start()
    {
        col = GetComponent<Collider2D>(); // Get the collider component
    }

    private void OnMouseDown()
    {
        startDragPosition = transform.position; // Store the initial position of the card when dragging starts
        transform.position = GetMousePositionInWorldSpace(); // Move the card to the mouse position in world space
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace(); // Continuously update the card's position to follow the mouse
    }

    private void OnMouseUp()
    {
        if (col == null) col = GetComponent<Collider2D>();
        col.enabled = false; // Disable the collider to prevent it from interfering with the drop logic
        Collider2D hitCollider = Physics2D.OverlapPoint((Vector2)transform.position); // Check if the card is dropped over a valid area (e.g., a slot)
        col.enabled = true; // Re-enable the collider after checking for valid drop area

        if (hitCollider != null && hitCollider.TryGetComponent(out ICardDropArea cardDropArea)) // Check if the collider belongs to a valid drop area (e.g., a slot)
        {
            cardDropArea.OnCardDropped(this); // Call the method on the drop area to handle the card being dropped
        }
        else
        {
            transform.position = startDragPosition; // If not dropped in a valid area, return the card to its original position
        }
    }

    private Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert the mouse position from screen space to world space
        p.z = 0; // Set the z-coordinate to 0 to ensure the card stays in the correct plane
        return p; // Return the calculated world position
    }

    // Update is called once per frame
    void Update()
    {
    }
}