using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float targetX;
    public float speed = 2f;
    public HandManager hm;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * speed);
        transform.position = pos;
        Debug.Log("Update works, timeScale = " + Time.timeScale);
        hm.ArrangeHand();
    }
}