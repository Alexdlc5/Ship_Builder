using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterNoise : MonoBehaviour
{
    //Basic Perlin Texture Credit: https://www.youtube.com/watch?v=bG0uEXV6aHQ
    public int width = 256;
    public int height = 256;
    public float scale;
    public float depth_gradient = 255.55555555f;
    private SpriteRenderer rawImage;
    public float offset_y;
    public float offset_x;
    public float water_current_speed = 1;
    public float water_current_speed_range = .02f;
    // Start is called before the first frame update
    private void Start()
    { 
        transform.localScale = (Vector3)new Vector2(120, 120);
        rawImage = GetComponent<SpriteRenderer>();
        rawImage.sprite = Sprite.Create(GenerateTexture(), new Rect(0, 0, width, height), Vector2.zero);
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x,y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }
    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;
        float sample;
        if (depth_gradient > 0)
        {
            sample = Mathf.PerlinNoise(xCoord, yCoord) + y / depth_gradient;
        }
        else
        {
            sample = Mathf.PerlinNoise(xCoord, yCoord);
        }
       
        return new Color(0, 0, sample);
    }
}
