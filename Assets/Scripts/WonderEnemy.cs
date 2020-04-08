using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderEnemy : Triangle
{
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        randomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,target) <= 0.2)
        {
            randomTarget();
        }
        movement();
    }

   

    
}
