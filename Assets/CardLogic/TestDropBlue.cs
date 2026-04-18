using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDropBlue : MonoBehaviour, ICardDropArea
{
    [SerializeField] private GameObject objectToSpawn; // The object to spawn when a card is dropped on the blue area
    
    public void OnCardDropped(Card card)
    {
        if (objectToSpawn != null)
        {
            // Instantiate the object at the position of the blue area
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No object assigned to spawn on card drop.");
        }
    }
}