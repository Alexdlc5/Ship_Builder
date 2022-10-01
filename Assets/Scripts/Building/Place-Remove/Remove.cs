using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
    public GameObject snapping_point;
    float timer = 0;
    GameObject closest_block;
    GameObject second_closest_block;
    GameObject[] edge_blocks;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Block") && Player_Selection.destruction_mode)
        {
            Snapping_Point_Manager.updatePoints();
            if (Input.GetMouseButtonDown(0))
            {
                edge_blocks = findEdgeBlocks();
                findClosestEdgeBlock();
            }
            if (Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            // if mouse held down for .5 sec
            if (timer > .5f)
            {
                Vector2 point_placement_position = closest_block.transform.position;
                //remove closest block (closest block locked during placment)
                Destroy(closest_block);
                closest_block = second_closest_block;
                //place snapping point 
                Instantiate(snapping_point).transform.position = point_placement_position;
                Snapping_Point_Manager.updatePoints();
                //set line renderer
                edge_blocks = findEdgeBlocks();
                findClosestEdgeBlock();
                setLineRen();
            }
            if (closest_block == null)
            {
                edge_blocks = findEdgeBlocks();
                findClosestEdgeBlock();
                setLineRen();
            }
        }
    }
    void setLineRen()
    {
        GameObject.Find("LineRenderer").GetComponent<Line_To_Nearest_Point>().setSecondPoint(closest_block.gameObject.transform);
    }
    private GameObject[] findEdgeBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        List<GameObject> edge_blocks = new List<GameObject>();
        //thru blocks
        for (int i = 0; i < blocks.Length; i++)
        {
            //thru points
            for (int j = 0; j < Snapping_Point_Manager.points.Length; j++)
            {
                //if point neighbors block
                if (Vector2.Distance(blocks[i].transform.position, Snapping_Point_Manager.points[j].transform.position) <= 1.1f)
                {
                    //block is an edge_block
                    edge_blocks.Add(blocks[i]);
                }
            }
        }
        return edge_blocks.ToArray();
    }
    private void findClosestEdgeBlock() 
    {
        if (edge_blocks[0] != null)
        {
            closest_block = edge_blocks[0];
            second_closest_block = edge_blocks[0];
        }
        else
        {
            closest_block = null;
        }
        for (int i = 0; i < edge_blocks.Length; i++)
        {
            //if current point closer
            if (edge_blocks[i].gameObject.GetComponent<Block>().distance <= closest_block.GetComponent<Block>().distance)
            {
                closest_block = edge_blocks[i];
            }
            else if (edge_blocks[i].gameObject.GetComponent<Block>().distance <= second_closest_block.GetComponent<Block>().distance)
            {
                second_closest_block = edge_blocks[i];
            }
        }
    }
}
