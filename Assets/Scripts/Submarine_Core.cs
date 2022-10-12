using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine_Core : MonoBehaviour
{
    public static bool submode = false;
    private Rigidbody2D rb;
    private Vector2 current_location;
    private Vector2 previous_location;
    public float acceleration_speed = 1;
    public float max_speed = 1;
    public float rotation_speed = 1;
    public float drag = 1;
    public static GameObject[] blocks;
    private Vector2 stop_location;
    // Start is called before the first frame update
    void Start()
    {
        updateBlocks();
        rb = GetComponent<Rigidbody2D>();
        stop_location = Vector2.zero;
        previous_location = Vector2.zero;
        current_location = Vector2.zero;
    }
    private void FixedUpdate()
    {
        if (!submode && transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0)))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rb.velocity = Vector3.zero;
            transform.position = stop_location;
        }
    }
    private void Update()
    {
        previous_location = current_location;
        current_location = transform.localPosition;
        Debug.DrawLine(previous_location, current_location);

        if (Input.GetKeyDown(KeyCode.E))
        {
            submode = !submode;
            if (!submode)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                rb.velocity = Vector3.zero;
                stop_location = transform.position;
            }
        }
        if (submode)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.MoveRotation(rb.rotation + rotation_speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.MoveRotation(rb.rotation - rotation_speed);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (speedInDirection("Horizontal", previous_location, current_location) > max_speed)
                {
                    rb.velocity = rb.velocity;
                }
                else
                {
                    rb.AddRelativeForce(new Vector2(acceleration_speed, 0));
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (speedInDirection("Horizontal", previous_location, current_location) < -max_speed)
                {
                    rb.velocity = rb.velocity;
                }
                else
                {
                    rb.AddRelativeForce(new Vector2(-acceleration_speed, 0));
                }
            }
            //drag
            if (rb.velocity.x > 0)
            {
                if (rb.velocity.x > drag)
                {
                    rb.velocity = new Vector2(rb.velocity.x - drag, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
            else if (rb.velocity.x < 0)
            {
                if (drag - rb.velocity.x > drag)
                {
                    rb.velocity = new Vector2(rb.velocity.x + drag, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
            if (rb.velocity.y > 0)
            {
                if (rb.velocity.y > drag)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - drag);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            }
            else if (rb.velocity.y < 0)
            {
                if (drag - rb.velocity.y > drag)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + drag);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            }
        }
    }
    //Not Working
    //public void updatePivot()
    //{
    //    //find furthest out blocks
    //    blocks = GameObject.FindGameObjectsWithTag("Block");
    //    Vector2[] furthest = new Vector2[4];
    //    for (int i = 0; i < blocks.Length; i++)
    //    {   
    //        //furthest up
    //        if (blocks[i].transform.position.y > furthest[0].y)
    //        {
    //            furthest[0] = blocks[i].transform.position;
    //        }
    //        //furthest down
    //        if (blocks[i].transform.position.y < furthest[1].y)
    //        {
    //            furthest[1] = blocks[i].transform.position;
    //        }
    //        //furthest right
    //        if (blocks[i].transform.position.x > furthest[2].x)
    //        {
    //            furthest[2] = blocks[i].transform.position;
    //        }
    //        //furthest left
    //        if (blocks[i].transform.position.x < furthest[3].x)
    //        {
    //            furthest[3] = blocks[i].transform.position;
    //        }
    //    }
    //    //find distance between horizontal and vertical points
    //    float vertical_middlepoint = Vector2.Distance(furthest[0], furthest[1]) / 2;
    //    float horizontal_middlepoint = Vector2.Distance(furthest[2], furthest[3]) / 2;
    //    //make new middle point vector
    //    Vector2 middle_point = new Vector2(furthest[3].x + horizontal_middlepoint, furthest[1].y + vertical_middlepoint);
    //    //unparent children to move game object
    //    if (transform.childCount > 0)
    //    {
    //        Transform[] children = GetComponentsInChildren<Transform>();
    //        foreach (Transform child in children)
    //        {
    //            child.parent = null;
    //        }
    //        //move to new point
    //        transform.position = middle_point;
    //        //reparent children
    //        foreach (Transform child in children)
    //        {
    //            child.parent = transform;
    //        }
    //    }
    //}
    public static void updateBlocks()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
    }
    private float speedInDirection(string direction, Vector2 v1, Vector2 v2)
    {
        if (direction.Equals("Vertical"))
        {
            return (v2.y - v1.y) / Time.deltaTime;
        }
        else if (direction.Equals("Horizontal"))
        {
            return (v2.x - v1.x) / Time.deltaTime;
        }
        else
        {
            return 0;
        }
    }
}
