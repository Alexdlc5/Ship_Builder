using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
    public GameObject snapping_point;
    private float timer = 0;
    public GameObject closest_block;
    public GameObject second_closest_block;
    public GameObject[] edge_blocks;
    private bool changed_last_frame = true;
    private Submarine_Core submarine;

    private void Start()
    {
        submarine = GameObject.Find("Submarine").GetComponent<Submarine_Core>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Block") && Player_Selection.destruction_mode)
        {
            Snapping_Point_Manager.updatePoints();
            Submarine_Core.updateBlocks();
            edge_blocks = findEdgeBlocks();
            findClosestEdgeBlock();
            changed_last_frame = false;
            //set line renderer
            setLineRen();
            if (Input.GetMouseButtonDown(0))
            {
                edge_blocks = findEdgeBlocks();
                findClosestEdgeBlock();
                //set line renderer
                setLineRen();
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
            if (timer > .5f && GameObject.FindGameObjectsWithTag("Block").Length > 1)
            {
                Vector2 point_placement_position = closest_block.transform.position;
                //remove the inside of closest block
                GameObject[] inside_blocks = GameObject.FindGameObjectsWithTag("InsideBlock");
                for (int i = 0; i < inside_blocks.Length; i++)
                {
                    if (closest_block.transform.position == inside_blocks[i].transform.position)
                    {
                        Destroy(inside_blocks[i]);
                        break;
                    }
                }
                //remove surrounding points
                //get array of points and block position
                GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
                Vector3 close_block_pos = closest_block.transform.position;
                for (int i = 0; i < points.Length; i++)
                {
                   //get positions of point
                    Vector2 point_pos = (Vector2)points[i].transform.position;
                    //checks if point is next to block being destroyed
                    bool isAbove = new Vector2(close_block_pos.x, close_block_pos.y + 1) == point_pos;
                    bool isBelow = new Vector2(close_block_pos.x, close_block_pos.y - 1) == point_pos;
                    bool isRight = new Vector2(close_block_pos.x + 1, close_block_pos.y) == point_pos;
                    bool isLeft = new Vector2(close_block_pos.x - 1, close_block_pos.y) == point_pos;
                    //if so destroy point
                    if (isAbove || isBelow || isRight || isLeft)
                    {
                        Destroy(points[i]);
                    }
                }
                //remove closest block 
                Destroy(closest_block);
                //place snapping point 
                GameObject newPoint = Instantiate(snapping_point);
                newPoint.GetComponent<Snapping_Point>().placed_by_destruction = true;
                newPoint.GetComponent<Snapping_Point>().held = false;
                newPoint.transform.position = point_placement_position;
                newPoint.transform.parent = GameObject.Find("Submarine").transform;
                //set closest block
                edge_blocks = findEdgeBlocks();
                findClosestEdgeBlock();
                Snapping_Point_Manager.updatePoints();
                timer = 0;
                //set line renderer
                setLineRen();
                //update center of sub
                submarine.updatePivot();
            }
        }
        else if (!changed_last_frame)
        {
            changed_last_frame = true;
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
