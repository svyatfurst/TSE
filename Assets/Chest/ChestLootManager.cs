using UnityEngine;

public class ChestLootManager : MonoBehaviour
{
    [Header("The Loot")]
    public GameObject falseCardPrefab;
    public GameObject semicolonCardPrefab;

    [Header("Connections")]
    public GameObject chestUIPanel;
    public ChestInteract chestHitboxScript;

    
    public void LootChest()
    {
        HandManager hand = FindObjectOfType<HandManager>();

        if (hand != null)
        {
            // 1. Spawn the physical prefabs
            GameObject newFalse = Instantiate(falseCardPrefab);
            GameObject newSemi = Instantiate(semicolonCardPrefab);

            // 2. Add them to the HandManager's list
            hand.cardsInHand.Add(newFalse.GetComponent<Card>());
            hand.cardsInHand.Add(newSemi.GetComponent<Card>());

            // 3. Force the hand to re-center
            hand.ArrangeHand();

            // 4. Close the Chest UI
            if (chestUIPanel != null)
            {
                chestUIPanel.SetActive(false);
            }

            // 5. Tell the physical chest it is empty so it can't be clicked again
            if (chestHitboxScript != null)
            {
                chestHitboxScript.DisableChest();
            }

            Debug.Log("Resources downloaded! Cards added to hand.");
        }
    }
}