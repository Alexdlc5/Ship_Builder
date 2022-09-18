using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping_Point_Manager : MonoBehaviour
{
    public Snapping_Point[] points;
    // Update is called once per frame
    void Start()
    {
        GameObject[] point_GO = GameObject.FindGameObjectsWithTag("Point");
        points = new Snapping_Point[point_GO.Length];
        for (int i = 0; i < point_GO.Length; i++) 
        {
            points[i] = point_GO[i].GetComponent<Snapping_Point>();
        }
    }
}
