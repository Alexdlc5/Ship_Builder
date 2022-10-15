using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perlin_Gen : MonoBehaviour
{
    public GameObject worldTile;
    public float perlin_scale = 1;
    public float generation_threshold = 1;
    public float generation_density_gradient = 1000.555f;
    public float grid_height = 300;
    public float grid_width = 200;
    float ran_offset_x;
    float ran_offset_y;
    // Start is called before the first frame update
    void Start()
    {
        ran_offset_x = Random.Range(0, .99999999999f);
        ran_offset_y = Random.Range(0, .99999999999f);

        //for if perlin needs changes in update method
        //GameObject[] old_tiles = GameObject.FindGameObjectsWithTag("Tile");
        //for (int i = 0; i < old_tiles.Length; i++)
        //{
        //    Destroy(old_tiles[i]);
        //}
        //thru y axis
        for (int y = 0; y < grid_height; y++)
        {
            //thru x axis
            for (int x = 0; x < grid_width; x++)
            {
                //if perlin value above threshold
                if (Mathf.PerlinNoise(ran_offset_x + x / perlin_scale, ran_offset_y + y / perlin_scale) <= generation_threshold - y / generation_density_gradient)
                {
                    GameObject tile = Instantiate(worldTile);
                    tile.transform.parent = transform;
                    tile.GetComponent<SpriteRenderer>().color = new Color(tile.GetComponent<SpriteRenderer>().color.r + y / grid_height,tile.GetComponent<SpriteRenderer>().color.g + y / grid_height, tile.GetComponent<SpriteRenderer>().color.b + y / grid_height, tile.GetComponent<SpriteRenderer>().color.a);
                    tile.transform.localPosition = new Vector2(x, y);
                }
            }
        }
    }
}
