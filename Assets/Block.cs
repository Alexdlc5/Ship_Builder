using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float distance;

    // Update is called once per frame
    void Update()
    {
        if (!Submarine_Core.submode)
        {
            distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
