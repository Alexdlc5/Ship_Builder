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
        distance_from_object = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (!held && distance_from_object > 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1 - (distance_from_object - 1) / 5);
        }
    }
}
