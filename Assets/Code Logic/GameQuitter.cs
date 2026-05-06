// Script to quit game at end
// By Harry Vowles
// 29339644
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    public void QuitGame()
    {
        // This will print a message in the Unity Console
        Debug.Log("Quit Button Pressed!");

        // This closes the actual built game (.exe, .app, etc.)
        Application.Quit();

        // If you are testing inside the Unity Editor, this line stops Play Mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}