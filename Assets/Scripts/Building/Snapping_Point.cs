using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Snapping_Point : MonoBehaviour
{
    public bool held = true;
    public float distance_from_object;
    // Update is called once per frame
    void Update()
    {
        //if (!gameObject.GetComponent<Submarine_Core>() || !GetComponentInParent<Block>())
        //{
        //    GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        //    bool connected = false;
        //    for (int i = 0; i < blocks.Length; i++)
        //    {
        //        if (Vector2.Distance(transform.position, blocks[i].transform.position) <= 1.1f)
        //        {
        //            connected = true;
        //        }
        //    }
        //    if (!connected)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
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
