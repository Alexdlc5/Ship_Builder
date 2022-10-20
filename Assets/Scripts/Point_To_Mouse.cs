using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_To_Mouse : MonoBehaviour
{
    public float angle_offset = -90;
    public bool isLight = false;
    // Update is called once per frame
    void Update()
    {
        //sub mode
        if (Submarine_Core.submode)
        {
            //get angle between mouse and object, rotate accordingly
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + angle_offset, Vector3.forward);
        }
        //build mode
        else
        {
            //face right
            transform.rotation = Quaternion.Euler(Vector3.forward);
        }
    }
}
