using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        //semiauto
        if (Submarine_Core.submode && !GetComponentInParent<Placeable_Object>() && Input.GetMouseButtonDown(0))
        {
            GameObject instantiatedProjectile = Instantiate(projectile);
            instantiatedProjectile.transform.position = transform.position;
            instantiatedProjectile.transform.rotation = transform.rotation;
        } 
    }
}
