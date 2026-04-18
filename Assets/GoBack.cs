using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject puzzleWindow;

    public void CloseWindow()
    {
        puzzleWindow.SetActive(false);
    }
}