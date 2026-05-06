using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public int page = 1;
    public GameObject window;
    public GameObject page1;

    public void NextPage()
    {
        page++;

        if (page > 1)
        {
            window.SetActive(false);
            page = 1;
        }
    }
}