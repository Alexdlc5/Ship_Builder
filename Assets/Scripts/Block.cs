using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float distance;
    private Remove remove;
    private SpriteRenderer spriteRenderer;
    private Line_To_Nearest_Point LTNP;
    public string block_function;
    private void Start()
    {
        LTNP = GameObject.Find("LineRenderer").GetComponent<Line_To_Nearest_Point>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        remove = GameObject.Find("Remove_Block").GetComponent<Remove>();
        GameObject.Find("Submarine").GetComponent<Submarine_Core>().addBlockStat();
    }
    // Update is called once per frame
    void Update()
    {
        if (!Submarine_Core.submode)
        {
            distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (Player_Selection.destruction_mode)
            {
                if (LTNP.points != null && LTNP.points[1] == transform)
                {
                    //make line visible and red
                    spriteRenderer.color = new Color(1, .1f, .1f, 1);
                }
                else
                {
                    //make line visible normal color
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                }
                //thru edge blocks
                bool hasNeighbor = false;
                for (int i = 0; i < remove.edge_blocks.Length; i++)
                {
                    //checks edge block locations against current points location
                    if (Vector2.Distance(remove.edge_blocks[i].transform.position, transform.position) <= 1.6f)
                    {
                        hasNeighbor = true;
                        continue;
                    }
                }
                //no Neighbor?
                if (!hasNeighbor)
                {
                    Destroy(gameObject);
                }
            }
        }
        else 
        {
            //make line visible normal color
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }

    }
}
