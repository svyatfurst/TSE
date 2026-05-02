using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    public GameObject window;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        window.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
