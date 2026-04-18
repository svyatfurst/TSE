using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("SequentialRoom");
        Debug.Log("DOOR CLICKED");
    }
}
