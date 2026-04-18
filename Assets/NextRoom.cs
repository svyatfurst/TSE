using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoom : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("FinalRoom");
        Debug.Log("DOOR CLICKED");
    }
}
