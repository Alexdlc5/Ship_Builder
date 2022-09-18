using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable_Object : MonoBehaviour
{
    public bool held = true;
    // Update is called once per frame
    void Update()
    {
        if (held)
        {
            Vector3 screenCoords = Input.mousePosition;
            screenCoords.z = Camera.main.nearClipPlane + 1;
            transform.position = Camera.main.ScreenToWorldPoint(screenCoords);
        }
        if (Input.GetMouseButton(0))
        {
            held = false;
        }
    }
}
