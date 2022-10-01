using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Snapping_Point_Manager : MonoBehaviour
{
    public static Snapping_Point[] points;
    private void Start()
    {
        updateForNewPlacement();
    }
    //if new block placed reset the array of points
    public static void updateForNewPlacement()
    { 
        GameObject[] point_GO = GameObject.FindGameObjectsWithTag("Point");
        int destroyed_point_count = 0;
        for (int i = 0; i < Submarine_Core.blocks.Length; i++)
        {
            for (int j = 0; j < point_GO.Length; j++)
            {
                //print("(" +point_GO[j].transform.position.x + ", " + point_GO[j].transform.position.y + ") : (" + Submarine_Core.blocks[i].transform.position.x + ", " + Submarine_Core.blocks[i].transform.position.y + ")");
                if (point_GO[j].transform.position == Submarine_Core.blocks[i].transform.position)
                {
                    if (point_GO[j].name.Equals("Submarine"))
                    {
                        point_GO[j].tag = "Untagged";
                    }
                    else
                    {
                        Destroy(point_GO[j]);
                        destroyed_point_count++;
                    }
                }
            }
        }
        points = new Snapping_Point[point_GO.Length];
        int index_skipped = 0;
        for (int i = 0; i < point_GO.Length; i++)
        {
            if (point_GO[i] == null)
            {
                index_skipped++;
            }
            else
            {
                points[i - index_skipped] = point_GO[i].GetComponent<Snapping_Point>();
            }
        }
    }
    public static void updatePoints()
    {
        GameObject[] point_GO = GameObject.FindGameObjectsWithTag("Point");
        points = new Snapping_Point[point_GO.Length];
        for (int i = 0; i < point_GO.Length; i++)
        {
            points[i] = point_GO[i].GetComponent<Snapping_Point>();
        }
    }
}

