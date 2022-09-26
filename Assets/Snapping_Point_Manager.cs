using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        points = new Snapping_Point[point_GO.Length];
        for (int i = 0; i < point_GO.Length; i++)
        {
            points[i] = point_GO[i].GetComponent<Snapping_Point>();
        }
    }
}
