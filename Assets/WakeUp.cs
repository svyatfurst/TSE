using UnityEngine;

public class WakeUp : MonoBehaviour
{
    public int page = 1;
    public GameObject window;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;

    public void NextPage()
    {
        page++;

        if (page == 2)
        {
            page1.SetActive(false);
            page2.SetActive(true);
            page3.SetActive(false);
        }
        else if (page == 3)
        {
            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(true);
        }
        else if (page > 3)
        {
            window.SetActive(false);
        }
    }
}