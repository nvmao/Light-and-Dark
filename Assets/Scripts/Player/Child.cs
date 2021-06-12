using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Triangle
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        randomTarget();
        player = FindObjectOfType<Player>().gameObject;
       
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform.position;
        movement();
    }

}
