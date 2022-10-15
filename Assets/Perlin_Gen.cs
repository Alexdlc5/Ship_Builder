using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Perlin_Gen : MonoBehaviour
{
    HashSet<Vector2> tile_positions;
    public GameObject worldTile;
    public GameObject slantworldTile;
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
        transform.position = new Vector2(-grid_width / 2, -grid_height * .8f);
        tile_positions = new HashSet<Vector2>();
        generateTiles();
        generate_slants();
    }
    void generateTiles()
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
                    tile.transform.localPosition = new Vector2(x, y);
                    tile_positions.Add(new Vector2(x, y));
                }
            }
        }
    }
    void generate_slants()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < tiles.Length; i++)
        {
            //no tile to the right
            if (!tile_positions.Contains(new Vector2(tiles[i].transform.localPosition.x + 1, tiles[i].transform.localPosition.y)) || !tile_positions.Contains(new Vector2(tiles[i].transform.localPosition.x - 1, tiles[i].transform.localPosition.y)))
            {
                //right side
                bool tile_down_to_the_right = tile_positions.Contains(new Vector2(tiles[i].transform.localPosition.x + 1, tiles[i].transform.localPosition.y - 1));
                bool tile_above_to_the_right = tile_positions.Contains(new Vector2(tiles[i].transform.localPosition.x + 1, tiles[i].transform.localPosition.y + 1));
                //left side
                bool tile_down_to_the_left = tile_positions.Contains(new Vector2(tiles[i].transform.localPosition.x - 1, tiles[i].transform.localPosition.y - 1));
                bool tile_above_to_the_left = tile_positions.Contains(new Vector2(tiles[i].transform.localPosition.x - 1, tiles[i].transform.localPosition.y + 1));
                // Example:           Places Slant
                // || <-- Tile        ||\  <--
                // || || <---TDTTR    ||||
                //no blocks to the right
                if (!tile_positions.Contains(new Vector2(tiles[i].transform.localPosition.x + 1, tiles[i].transform.localPosition.y)))
                {
                    //right down slant tile
                    if (tile_down_to_the_right)
                    {
                        GameObject tile = Instantiate(slantworldTile);
                        tile.transform.parent = transform;
                        tile.transform.localPosition = new Vector2(tiles[i].transform.localPosition.x + 1, tiles[i].transform.localPosition.y);
                    }
                    //upside down slant tile
                    else if (tile_above_to_the_right)
                    {
                        GameObject tile = Instantiate(slantworldTile);
                        tile.transform.parent = transform;
                        tile.transform.localPosition = new Vector2(tiles[i].transform.localPosition.x + 1, tiles[i].transform.localPosition.y);
                        tile.transform.rotation = Quaternion.Euler(tile.transform.rotation.x, tile.transform.rotation.y, 270);
                    }
                }
                //no blocks to the left
                if (!tile_positions.Contains(new Vector2(tiles[i].transform.localPosition.x - 1, tiles[i].transform.localPosition.y)))
                {
                    //left down slant tile
                    if (tile_down_to_the_left)
                    {
                        GameObject tile = Instantiate(slantworldTile);
                        tile.transform.parent = transform;
                        tile.transform.localPosition = new Vector2(tiles[i].transform.localPosition.x - 1, tiles[i].transform.localPosition.y);
                        tile.transform.rotation = Quaternion.Euler(tile.transform.rotation.x, tile.transform.rotation.y, 90);
                    }
                    //upside down left slant tile
                    else if (tile_above_to_the_left)
                    {
                        GameObject tile = Instantiate(slantworldTile);
                        tile.transform.parent = transform;
                        tile.transform.localPosition = new Vector2(tiles[i].transform.localPosition.x - 1, tiles[i].transform.localPosition.y);
                        tile.transform.rotation = Quaternion.Euler(tile.transform.rotation.x, tile.transform.rotation.y, 180);
                    }
                }
            }
        }
    }


}
