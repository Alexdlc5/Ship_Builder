using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable_Object : MonoBehaviour
{
    public bool held = true;
    public GameObject[] surrounding_points;
    private Snapping_Point closest_point;
    private Snapping_Point second_closest_point;
    private float placement_timer = 0;

    private void Start()
    {
        for (int i = 0; i < surrounding_points.Length; i++)
        {
            surrounding_points[i].tag = "Untagged";
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Player_Selection.destruction_mode)
        {
            Destroy(gameObject);
        }
        Snapping_Point_Manager.updateForNewPlacement();
        GetComponent<BoxCollider2D>().enabled = false;
        if (held)
        {
            //follow the mouse while held
            Vector3 screenCoords = Input.mousePosition;
            screenCoords.z = Camera.main.nearClipPlane + 1;
            transform.position = Camera.main.ScreenToWorldPoint(screenCoords);
            setVisibility(false);
        }
        if (Submarine_Core.submode == false && Input.GetMouseButtonDown(0) && placement_timer > .1f)
        {
            setVisibility(true);

            placement_timer = 0;
            held = false;
            //place at closest point 
            transform.position = closest_point.transform.position;
            if (closest_point.gameObject.name.Equals("Submarine"))
            {
                closest_point.gameObject.tag = "Untagged";
            }
            else
            {
                Destroy(closest_point.gameObject);
            }
            closest_point = second_closest_point;
            for (int i = 0; i < surrounding_points.Length; i++)
            {
                surrounding_points[i].tag = "Point";
            }
            Snapping_Point_Manager.updateForNewPlacement();
            setLineRen();
            //changes to block, updates subcore, destroys placeable script
            gameObject.tag = "Block";
            Submarine_Core.updateBlocks();
            gameObject.transform.parent = GameObject.Find("Submarine").transform;
            //GetComponent<BoxCollider2D>().enabled = true;
            //creates new placable object to place next
            GameObject placed_object = Instantiate(Player_Selection.current_placable);
            placed_object.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            second_closest_point = Snapping_Point_Manager.points[0];
        }
        else
        {
            closest_point = null;
            second_closest_point = null;
        }
        for (int i = 0; i < Snapping_Point_Manager.points.Length; i++)
        {
            //if current point closer
            if (Snapping_Point_Manager.points[i].distance_from_object <= closest_point.distance_from_object)
            {
                if (checkSnappingPointOcuppation(i) != null)
                {
                    closest_point = checkSnappingPointOcuppation(i);
                }
            }
            else if (Snapping_Point_Manager.points[i].distance_from_object <= second_closest_point.distance_from_object)
            {
                if (checkSnappingPointOcuppation(i) != null)
                {
                    second_closest_point = checkSnappingPointOcuppation(i);
                }
            }
        }
    }
    Snapping_Point checkSnappingPointOcuppation(int i)
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
            return Snapping_Point_Manager.points[i];
        }
        else
        {
            return null;
        }
    }
    //updates second point of line renderer
    void setLineRen()
    {
        findClosestSnappingPoint();
        GameObject.Find("LineRenderer").GetComponent<Line_To_Nearest_Point>().setSecondPoint(closest_point.gameObject.transform);
    }
    void setVisibility(bool visibility)
    {
        int isvisible = 0;
        if (visibility)
        {
            isvisible = 1;
        }
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, isvisible);
        for (int i = 0; i < surrounding_points.Length; i++)
        {
            surrounding_points[i].GetComponent<SpriteRenderer>().color = new Color(surrounding_points[i].GetComponent<SpriteRenderer>().color.r, surrounding_points[i].GetComponent<SpriteRenderer>().color.g, surrounding_points[i].GetComponent<SpriteRenderer>().color.b, isvisible);
            surrounding_points[i].GetComponent<Snapping_Point>().held = !visibility;
        }
    }
}