using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Snapping_Point : MonoBehaviour
{
    public float distance_from_object;
    // Update is called once per frame
    void Update()
    {
        distance_from_object = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
