using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable_Object : MonoBehaviour
{
    public bool held = true;
    public GameObject[] surrounding_points;
    private Snapping_Point closest_point;
    private float placement_timer = 0;

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
        if (Input.GetMouseButton(0) && placement_timer > .4f)
        {
            placement_timer = 0;
            held = false;
            //place at closest point then delete point
            transform.position = closest_point.transform.position;
            if (!closest_point.gameObject.name.Equals("Submarine"))
            {
                Destroy(closest_point.gameObject);
            }
            else
            {
                closest_point.gameObject.tag = "Untagged";
                Destroy(closest_point);
            }
            print("out");
            if (Snapping_Point_Manager.points != null)
            {
                for (int i = 0; i < surrounding_points.Length; i++)
                {
                    if ((Vector2) surrounding_points[i].transform.position == new Vector2(0,0))
                    {
                        Destroy(surrounding_points[i]);
                    }
                    else
                    {
                        for (int j = 0; j < Snapping_Point_Manager.points.Length; j++)
                        {
                            //print("(" + surrounding_points[i].transform.position.x + ", " + surrounding_points[i].transform.position.y + ") : (" + Snapping_Point_Manager.points[j].transform.position.x + ", " + Snapping_Point_Manager.points[j].transform.position.y + ")");
                            surrounding_points[i].transform.parent = null;
                            if (Snapping_Point_Manager.points[j] != null && (Vector2)surrounding_points[i].transform.position == (Vector2)Snapping_Point_Manager.points[j].transform.position)
                            {
                                Destroy(surrounding_points[i]);
                            }
                            else
                            {
                                surrounding_points[i].transform.parent = gameObject.transform;
                            }
                        }
                    }
                }
            }
            print("in");
            Snapping_Point_Manager.updateForNewPlacement();
            setLineRen();
            //creates new placable object to place next
            GameObject placed_object = Instantiate(Player_Selection.current_placable);
            placed_object.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //changes to block, updates subcore, destroys placeable script
            gameObject.tag = "Block";
            Submarine_Core.updateBlocks();
            gameObject.transform.parent = GameObject.Find("Submarine").transform;
            Destroy(gameObject.GetComponent<Placeable_Object>());
        }
        else 
        {
            setLineRen();
        }
        //increment timer
        placement_timer += Time.deltaTime;
    }

    void findClosestSnappingPoint()
    {
        if (Snapping_Point_Manager.points[0] != null)
        {
            closest_point = Snapping_Point_Manager.points[0];
        }
        else
        {
            closest_point = null;
        }
        for (int i = 0; i < Snapping_Point_Manager.points.Length; i++)
        {
            //if current point closer; make closest point
            if (closest_point.distance_from_object >= Snapping_Point_Manager.points[i].distance_from_object)
            {
                bool location_occupied = false;
                for (int j = 0; j < Submarine_Core.blocks.Length; j++)
                {
                    if (Snapping_Point_Manager.points[i] == null || Snapping_Point_Manager.points[i].gameObject.transform.position == Submarine_Core.blocks[j].transform.position)
                    {
                        location_occupied = true;
                    }
                }
                if (!location_occupied)
                {
                    closest_point = Snapping_Point_Manager.points[i];
                }
            }
        }
    }
    //updates second point of line renderer
    void setLineRen()
    {
        findClosestSnappingPoint();
        GameObject.Find("LineRenderer").GetComponent<Line_To_Nearest_Point>().setSecondPoint(closest_point.gameObject.transform);
    }
}
