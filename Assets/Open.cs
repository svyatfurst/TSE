using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open : MonoBehaviour
{
    public CameraMove cam;
    public float newX = 10f;

    void OnMouseDown()
    {
        cam.targetX = newX;
        Debug.Log("DOOR CLICKED");
    }
}
