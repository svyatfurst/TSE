using System.Collections;
using UnityEngine;

public class WiresDropArea : MonoBehaviour, ICardDropArea
{
    public GameObject openDoor;
    public Animator animator;

    public bool OnCardDropped(Card card)
    {
        Debug.Log("Wires dropped: " + card.name);

        card.transform.position = transform.position;

        // Changing the color of each red wire to green
        foreach (var sr in GameObject.Find("Power System/Wires/Red Wire").GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = Color.green;
        }

        animator.SetBool("Play", true);
        StartCoroutine(DelayAction());

        return true;
    }

    IEnumerator DelayAction()
    {
        yield return new WaitForSeconds(1f);

        openDoor.SetActive(true);
    }
}