using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine_Core : MonoBehaviour
{
    public static GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        updateBlocks();
    }
    public static void updateBlocks()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
    }
}
