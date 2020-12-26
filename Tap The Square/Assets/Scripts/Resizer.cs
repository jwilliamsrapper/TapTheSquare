using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    Vector3 temp;
    bool growing;

    private void Start()
    {
        growing = true;
    }


    void Update()
    {

        // shrinks the squares
        temp = transform.localScale;
        if (temp.x <= .2f && temp.y <= .2f && growing)
        {
            temp.x -= .00021f;
            temp.y -= .00021f;
            transform.localScale = temp;

            if (temp.x <= .1f && temp.y <= .1f)
            {
                growing = false;
            }

        }

        // grows the squares
        if (temp.x >= .07f && temp.y >= .07f && growing == false)
        {
            temp.x += .00021f;
            temp.y += .00021f;
            transform.localScale = temp;

            if (temp.x >= .16f && temp.y >= .16f)
            {
                growing = true;
            }

        }


    }
}
