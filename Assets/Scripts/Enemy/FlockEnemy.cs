using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockEnemy : Triangle
{
    private FlockEnemy[] neightbours;

    private float alignRadius= 3f;
    private float coheRadius = 1f;
    private float sepaRadius = 2f;

    private Box[] boxs;

    // Start is called before the first frame updatef
    void Start()
    {
        boxs = FindObjectsOfType<Box>();

        base.Start();
        //speed = Random.Range(3, 5);

        neightbours = FindObjectsOfType<FlockEnemy>();

        float x = Random.Range(-16, 16);
        float y = Random.Range(-9, 9);
        body.velocity = new Vector2(x, y).normalized * speed;


    }

    // Update is called once per frame
    void Update()
    {
        edges();
        movement();
    }

    private void movement()
    {
        velocity = body.velocity;

        Vector2 steering = Vector2.zero;

        steering = steering + this.alignment();
        steering = steering + this.cohesion();
        steering = steering + this.separation();
        //steering = steering + avoidance(boxs);

        steering = Vector2.ClampMagnitude(steering, seekForce);

        velocity = Vector2.ClampMagnitude(velocity + steering, speed);

        lookAt(velocity);
        body.velocity = velocity;
    }

    private void edges()
    {
        if(transform.position.x < -20)
        {
            transform.position = new Vector3(20, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 20)
        {
            transform.position = new Vector3(-20, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, 11, transform.position.z);
        }
        else if (transform.position.y > 11)
        {
            transform.position = new Vector3(transform.position.x, -11, transform.position.z);
        }
    }

    private Vector2 alignment()
    {
        Vector2 v = new Vector2();
        int neighborCount = 0;

        foreach(var triangle in neightbours)
        {
            float d = Vector2.Distance(transform.position, triangle.transform.position);
            if (triangle != this 
               && d < alignRadius)
            {
                v += triangle.velocity;
                neighborCount++;
            }
        }
        
        if(neighborCount > 0)
        {
            v = v / neighborCount;

            v = v.normalized;
        }

        return v;
    }

    private Vector2 cohesion()
    {
        Vector2 v = new Vector2();
        Vector2 centerOfMass = new Vector2();

        int neighborCount = 0;

        foreach (var triangle in neightbours)
        {
            float d = Vector2.Distance(transform.position, triangle.transform.position);
            if (triangle != this
                && d < coheRadius)
            {
                centerOfMass += (Vector2)triangle.transform.position;
                neighborCount++;
            }
        }
        if (neighborCount > 0)
        {
            centerOfMass = centerOfMass / neighborCount;
            v = centerOfMass - (Vector2)transform.position;
            v = v.normalized;
        }


        return v;
    }

    private Vector2 separation()
    {
        Vector2 v = new Vector2();
        int neighborCount = 0;

        foreach (var triangle in neightbours)
        {
            float d = Vector2.Distance(transform.position, triangle.transform.position);

            if (triangle != this
                && d < sepaRadius)
            {
                Vector2 diff = (Vector2)transform.position - (Vector2)triangle.transform.position;
                float scale = diff.magnitude/(float)Mathf.Sqrt(sepaRadius);

                v = diff.normalized / scale;

                neighborCount++;
            }
        }

        return v;
    }

}
