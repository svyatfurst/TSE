using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public int page = 1;
    public GameObject window;
    public GameObject page1;
    public GameObject page2;

    public void NextPage()
    {
        page++;

        if (page == 2)
        {
            page1.SetActive(false);
            page2.SetActive(true);
        }
        else if (page > 2)
        {
            window.SetActive(false);
        }
    }
}