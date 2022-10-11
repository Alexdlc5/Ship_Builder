using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Snapping_Point : MonoBehaviour
{
    public bool held = true;
    public bool placed_by_destruction = false;
    public float distance_from_object;
    private Remove remove;
    private void Start()
    {
        remove = GameObject.Find("Remove_Block").GetComponent<Remove>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!Submarine_Core.submode && Player_Selection.destruction_mode && placed_by_destruction)
        {
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
        distance_from_object = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Submarine_Core.submode == true)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0);
        }
        else if (!held && distance_from_object > 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1 - (distance_from_object - 1) / 5);
        }
    }
}
