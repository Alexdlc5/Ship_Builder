using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterCabinManager : MonoBehaviour
{
    public Sprite[] CabinTypes;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!Submarine_Core.submode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
                int neigbor_count = 0;
                for (int i = 0; i < blocks.Length; i++)
                {
                    if (Vector2.Distance(blocks[i].transform.position, transform.position) == 1f)
                    {
                        neigbor_count++;
                    }
                }
                if (neigbor_count >= 3)
                {
               
                    //blocked in sprite
                    spriteRenderer.sprite = CabinTypes[1];
                }
                else
                {
                    //external sprite
                    spriteRenderer.sprite = CabinTypes[0];
                }
            }
        }
    }
}
