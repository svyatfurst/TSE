using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float targetX;
    public float speed = 2f;
    public HandManager hm;

    void Update()
    {
        // Check if the camera is already at the target position. 
        // We use 0.01f as a tiny buffer because Lerp rarely hits EXACTLY the target number.
        if (Mathf.Abs(transform.position.x - targetX) > 0.01f)
        {
            // 1. Move the camera
            Vector3 pos = transform.position;
            pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * speed);
            transform.position = pos;

            // 2. Update the hand ONLY while the camera is actively moving
            if (hm != null)
            {
                hm.ArrangeHand();
            }
        }
        else
        {
            // Snap it perfectly to the target just to be clean, 
            // but we stop calling ArrangeHand() so the hitboxes can settle!
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
    }
}