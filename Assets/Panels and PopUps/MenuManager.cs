using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    [Tooltip("Drag your Start Menu Panel here")]
    public GameObject startMenuPanel;

    // called when the PLAY button is clicked
    public void StartGame()
    {
        // hide the menu so the player can see the game
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(false);
        }

        Debug.Log("Game Started! Welcome to the Tutorial Room.");

    }

    // This is called when EITHER of the EXIT buttons are clicked
    public void QuitGame()
    {
        Debug.Log("Exiting Game...");

        // This quits the game when you actually build and play it 
        Application.Quit();

        // you can test if the button works
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}