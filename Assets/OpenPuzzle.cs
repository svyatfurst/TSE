using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour
{
    public GameObject puzzleWindow;

    void OnMouseDown()
    {
        puzzleWindow.SetActive(true);
    }
}