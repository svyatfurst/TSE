using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWires : MonoBehaviour
{
    public GameObject WiresCard;
    public GameObject Wires;

    void OnMouseDown()
    {
        WiresCard.SetActive(true);
        WiresCard.transform.position = new Vector3(0, -24, 0);
        Wires.SetActive(false);
    }
}
