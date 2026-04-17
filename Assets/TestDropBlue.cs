using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDropBlue : MonoBehaviour, ICardDropArea
{
    [SerializeField] private GameObject objectToSpawn; // The object to spawn when a card is dropped on the blue area
    
    public void OnCardDropped(Card card)
    {
        Destroy(card.gameObject); // Destroy the card that was dropped on the blue area
        Instantiate(objectToSpawn, transform.position, transform.rotation); // Spawn the specified object at the position and rotation of the blue area
    }
}