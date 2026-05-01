using UnityEngine;

public class InteractableNote : MonoBehaviour
{
    [Header("UI Menu Hookup")]
    [Tooltip("Drag the Note UI Panel from your Hierarchy into this slot")]
    public GameObject noteUIPanel;

    // This built-in Unity function fires when a player clicks/taps an object with a Collider
    private void OnMouseDown()
    {
        if (noteUIPanel != null)
        {
            // Show the pop-up menu
            noteUIPanel.SetActive(true);

            // Pause the game while the menu is open so the player can't keep dragging wires
            Time.timeScale = 0f; 
        }
        else
        {
            Debug.LogWarning("Note Panel missing! Did you drag it into the inspector?");
        }
    }

    // hook this function up to a UI "Close" button
    public void CloseNoteMenu()
    {
        if (noteUIPanel != null)
        {
            // Hide the pop-up menu
            noteUIPanel.SetActive(false);

            // Unpause the game when closed
            Time.timeScale = 1f;
        }
    }
}
