using System.Collections;
using UnityEngine;

public class WiresDropArea : MonoBehaviour, ICardDropArea
{
    public GameObject openDoor;
    public Animator animator;
    public GameObject fixedWires;
    public GameObject sparks;
    public GameObject cardPrefab;
    public GameObject powered;

    public bool OnCardDropped(Card card)
    {
        Debug.Log("Wires dropped: " + card.name);

        card.transform.position = transform.position;
        cardPrefab.SetActive(false);

        fixedWires.SetActive(true);
        sparks.SetActive(false);
        powered.SetActive(true);

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