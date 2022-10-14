using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnabledDuringSubMode : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Submarine_Core.submode)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (!Submarine_Core.submode)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
