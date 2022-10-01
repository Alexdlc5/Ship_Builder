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
