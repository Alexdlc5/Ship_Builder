using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine_Core : MonoBehaviour
{
    private bool submode = false;
    private Rigidbody2D rb;
    public float movement_speed = 1;
    public float rotation_speed = 1;
    public static GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        updateBlocks();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!submode && transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0)))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rb.velocity = Vector3.zero;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            submode = !submode;
            if (!submode)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                rb.velocity = Vector3.zero;
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
                rb.AddRelativeForce(new Vector2(movement_speed,0));
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddRelativeForce(new Vector2(-movement_speed, 0));
            }
        }
    }
    public static void updateBlocks()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
    }
}
