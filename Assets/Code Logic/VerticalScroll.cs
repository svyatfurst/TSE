using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    public MovingFloor movingFloor;

    public float pixelsPerSecond = 8f;
    public int textureHeight = 50;

    private Renderer rend;
    private float pixelOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (movingFloor.isMoving)
        {
            pixelOffset += pixelsPerSecond * Time.deltaTime;

            int wholePixels = Mathf.FloorToInt(pixelOffset);

            float uvOffsetY = (float)wholePixels / textureHeight;

            rend.material.mainTextureOffset = new Vector2(0, uvOffsetY);
        }
        
    }
}