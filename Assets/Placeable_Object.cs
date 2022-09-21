using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable_Object : MonoBehaviour
{
    public bool held = true;
    public Snapping_Point_Manager point_manager;

    private void Start()
    {
        point_manager = GameObject.FindGameObjectWithTag("SnappingPointsManager").GetComponent<Snapping_Point_Manager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (held)
        {
            //follow the mouse while held
            Vector3 screenCoords = Input.mousePosition;
            screenCoords.z = Camera.main.nearClipPlane + 1;
            transform.position = Camera.main.ScreenToWorldPoint(screenCoords);
        }
        if (Input.GetMouseButton(0))
        {
            //find closest point and snap to it when dropped
            held = false;
            Snapping_Point closest_point = point_manager.points[0];
            for (int i = 0; i < point_manager.points.Length; i++)
            {
                //if current point closer; make closest point
                if (closest_point.distance_from_object >= point_manager.points[i].distance_from_object)
                {
                    closest_point = point_manager.points[i];
                }
            }
            //place at closest point
            transform.position = closest_point.transform.position;
        }
    }
}
