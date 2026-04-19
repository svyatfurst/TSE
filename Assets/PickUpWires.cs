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
        Wires.SetActive(false);
    }
}
