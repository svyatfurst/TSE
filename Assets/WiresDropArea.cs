using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WiresDropArea : MonoBehaviour, ICardDropArea
{
    public GameObject openDoor;

    public bool OnCardDropped(Card card)
    {
        Debug.Log("Wires dropped: " + card.name);

        card.transform.position = transform.position;

        //Changing the color of each red wire to red
        foreach (var sr in GameObject.Find("Power System/Wires/Red Wire").GetComponentsInChildren<SpriteRenderer>())
            sr.color = Color.green;

        openDoor.SetActive(true);

        return true; // accedpted card
    }
}
