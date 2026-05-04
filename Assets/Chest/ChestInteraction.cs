using UnityEngine;

public class ChestInteract : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject chestUIPanel;

    private bool isLooted = false;

    private void OnMouseDown()
    {
        // Only open if we haven't looted it yet
        if (!isLooted && chestUIPanel != null)
        {
            chestUIPanel.SetActive(true);
            Debug.Log("Establishing connection to secure cache...");
        }
    }

    // The Loot Manager will call this later
    public void DisableChest()
    {
        isLooted = true;
        Debug.Log("Cache empty. Connection severed.");
    }

    public void SetIsLooted(bool _isLooted)
    {
        isLooted = _isLooted;
    }
}