using UnityEngine;

public class Card : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 startDragPosition;
    private bool isTargeting = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Hide the line at the start
        lineRenderer.positionCount = 0;
    }

    private void OnMouseDown()
    {
        isTargeting = true;
        startDragPosition = transform.position;
        lineRenderer.positionCount = 2; // Start and End points
    }

    private void OnMouseDrag()
    {
        if (isTargeting)
        {
            Vector3 mousePos = GetMousePositionInWorldSpace();

            // Set the line points
            lineRenderer.SetPosition(0, startDragPosition); // Start at card
            lineRenderer.SetPosition(1, mousePos);          // End at mouse
        }
    }

    private void OnMouseUp()
    {
        isTargeting = false;
        lineRenderer.positionCount = 0; // Hide the line

        Vector3 mousePos = GetMousePositionInWorldSpace();

        // Check what is UNDER THE MOUSE, not under the card
        Collider2D hitCollider = Physics2D.OverlapPoint((Vector2)mousePos);

        if (hitCollider != null && hitCollider.TryGetComponent(out ICardDropArea cardDropArea))
        {
            // Move the card to the target location
            transform.position = hitCollider.transform.position;
            cardDropArea.OnCardDropped(this);
        }
        else
        {
            // Return to original spot if we missed
            transform.position = startDragPosition;
        }
    }

    private Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0;
        return p;
    }

    void Update()
    {
        if (isTargeting)
        {
            // This slides the texture along the line
            float offset = Time.time * -2.0f;
            lineRenderer.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}