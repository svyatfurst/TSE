//BrokenWire.cs
// Logic for dropping wire card on wire box
// by Harry Vowles
// 29339644
using System.Collections;
using UnityEngine;

public class BrokenWire : MonoBehaviour, ICardDropArea
{
    [SerializeField] private CardTrait requiredTrait = CardTrait.Wire;
    public GameObject openDoor;
    public Animator animator;
    public GameObject fixedWires;
    public GameObject sparks;
    public GameObject powered;

    public bool OnCardDropped(Card droppedCard)
    {
        // 1. Check if the card has data and matches the required trait
        if (droppedCard.cardData != null && droppedCard.cardData.cardTrait == requiredTrait)
        {
            Debug.Log("Success! The wire was fixed!");

            // Destroy card
            Destroy(droppedCard.gameObject);
            fixedWires.SetActive(true);
            sparks.SetActive(false);
            powered.SetActive(true);

            animator.SetBool("Play", true);
            StartCoroutine(DelayAction());

            return true; // Tells the Card.cs script it was accepted
        }
        else
        {
            Debug.Log("Wrong card! This needs a Wire card.");
            return false; // Tells the card to snap back
        }
    }

    IEnumerator DelayAction()
    {
        yield return new WaitForSeconds(1f);

        openDoor.SetActive(true);
    }
}

