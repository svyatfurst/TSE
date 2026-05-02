using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("What card does this give you?")]
    public GameObject cardPrefabToGive;

    private void OnMouseDown()
    {
        // 1. Find your HandManager in the scene
        HandManager hand = FindObjectOfType<HandManager>();

        if (hand != null && cardPrefabToGive != null)
        {
            // 2. Spawn the 2D Card Prefab into the world
            GameObject newCardObj = Instantiate(cardPrefabToGive);

            // 3. Grab the Card script attached to it
            Card newCardScript = newCardObj.GetComponent<Card>();

            // 4. Add it to the player's hand list
            hand.cardsInHand.Add(newCardScript);

            // 5. Force the hand to recalculate positions and slide it into view
            hand.ArrangeHand();

            // 6. Destroy the physical wire on the floor!
            Destroy(gameObject);

            Debug.Log("Picked up item! Wire Card added to hand.");
        }
    }
}