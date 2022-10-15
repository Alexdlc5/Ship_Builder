using System;
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

    public float propeller_count = 0;
    public float engine_count = 0;
    public float cabin_count = 0;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            submode = !submode;
            if (!submode)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                rb.velocity = Vector3.zero;
                stop_location = transform.position;
                GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
                for (int i = 0; i < blocks.Length; i++)
                {
                    blocks[i].GetComponent<Collider2D>().enabled = false;
                }
            }
            else
            {
                GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
                for (int i = 0; i < blocks.Length; i++)
                {
                    blocks[i].GetComponent<Collider2D>().enabled = true;
                }
            }
        }
        //change or fix movment
        if (submode)
        {
            bool any_key_pressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift);
            if (any_key_pressed)
            {
                rb.angularVelocity = 0;
            }
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
                    rb.AddRelativeForce(Vector2.right * acceleration_speed * Time.deltaTime * 200, 0);
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
                    rb.AddRelativeForce(Vector2.left * acceleration_speed * Time.deltaTime * 200, 0);
                }
            }
        }   
        //drag
        //if (!(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift)))
        //{
        //    //x drag
        //    if (rb.velocity.x > 0)
        //    {
        //        if (rb.velocity.x > drag)
        //        {
        //            rb.velocity = new Vector2(rb.velocity.x - drag, rb.velocity.y);
        //        }
        //        else
        //        {
        //            rb.velocity = new Vector2(0, rb.velocity.y);
        //        }
        //    }
        //    else if (rb.velocity.x < 0)
        //    {
        //        if (drag - rb.velocity.x > drag)
        //        {
        //            rb.velocity = new Vector2(rb.velocity.x + drag, rb.velocity.y);
        //        }
        //        else
        //        {
        //            rb.velocity = new Vector2(0, rb.velocity.y);
        //        }
        //    }
        //    //y drag
        //    if (rb.velocity.y > 0)
        //    {
        //        if (rb.velocity.y > drag)
        //        {
        //            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - drag);
        //        }
        //        else
        //        {
        //            rb.velocity = new Vector2(rb.velocity.x, 0);
        //        }
        //    }
        //    else if (rb.velocity.y < 0)
        //    {
        //        if (drag - rb.velocity.y > drag)
        //        {
        //            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + drag);
        //        }
        //        else
        //        {
        //            rb.velocity = new Vector2(rb.velocity.x, 0);
        //        }
        //    }
        //}
    }
    public void addBlockStat()
    {
        //reset counts
        cabin_count = 0;
        engine_count = 0;
        propeller_count = 0;

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        //update block counts
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].GetComponent<Block>().block_function.Equals("Cabin"))
            {
                cabin_count++;
            }
            else if (blocks[i].GetComponent<Block>().block_function.Equals("Propeller"))
            {
                propeller_count++;
            }
            else if (blocks[i].GetComponent<Block>().block_function.Equals("Engine"))
            {
                engine_count++;
            }
        }
        //update submarine stats
        acceleration_speed = 2 + (propeller_count / 10);
        max_speed = 1 + (engine_count / 10);
        drag = 0.05f + ((cabin_count + engine_count) / 1000);
    }
    
    //pivot updater v2
    public void updatePivot()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        //creates sorted list of y values
        List<float> y_values = new List<float>();
        for (int i = 0; i < blocks.Length; i++)
        {
            if (y_values.Count > 0)
            {
                bool value_added = false;
                for (int j = 0; j < y_values.Count; j++)
                {
                    if (blocks[i].transform.position.y < y_values[j])
                    {
                        y_values.Insert(j, blocks[i].transform.position.y);
                        value_added = true;
                        break;
                    }
                }
                if (!value_added)
                {
                    y_values.Add(blocks[i].transform.position.y);
                }
            }
            else
            {
                y_values.Add(blocks[i].transform.position.y);
            }
        }
        //creates sorted list of x values
        List<float> x_values = new List<float>();
        for (int i = 0; i < blocks.Length; i++)
        {
            if (x_values.Count > 0)
            {
                bool value_added = false;
                for (int j = 0; j < x_values.Count; j++)
                {
                    if (blocks[i].transform.position.x < x_values[j])
                    {
                        x_values.Insert(j, blocks[i].transform.position.x);
                        value_added = true;
                        break;
                    }
                }
                if (!value_added)
                {
                    x_values.Add(blocks[i].transform.position.x);
                }
            }
            else
            {
                x_values.Add(blocks[i].transform.position.x);
            }
        }
        //find medians
        float x_median = getMedian(x_values);
        float y_median = getMedian(y_values);
        //unparent children to move game object
        if (transform.childCount > 0)
        {
            Transform[] children = GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                child.parent = null;
            }
            //move to new point
            transform.position = new Vector2(x_median, y_median); 
            //reparent children
            foreach (Transform child in children)
            {
                child.parent = transform;
            }
        }
    }
    private float getMedian(List<float> sorted_data)
    {
        //odd count
        if (sorted_data.Count % 2 > 0)
        {
            //return median
            return sorted_data[sorted_data.Count / 2];
        }
        //even count
        else
        {
            //return avg of two medians
            return ((sorted_data[sorted_data.Count / 2 - 1] + sorted_data[sorted_data.Count / 2]) /2);
        }
    }
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
