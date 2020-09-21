using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockEnemy : Triangle,mao.ICanDisable
{
    [SerializeField]
    private float alignRadius = 5f;
    [SerializeField]
    private float coheRadius = 1f;
    [SerializeField]
    private float sepaRadius = 2f;


    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask selfMask;


    float neightBourRadius = 5;

    public float minX = -90;
    public float maxX = 95;
    public float minY = -24;
    public float maxY = 28;

    BlurOnAwaken blurOnAwaken;

    private void Awake()
    {
        blurOnAwaken = new BlurOnAwaken(GetComponent<SpriteRenderer>());
        StartCoroutine(blurOnAwaken.wait());
    }

    // Start is called before the first frame updatef
    void Start()
    {
        base.Start();
        //speed = Random.Range(3, 5);

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
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

        steering += alignment() + separation() + cohesion();
        steering += avoidance(layerMask);

        steering = Vector2.ClampMagnitude(steering, seekForce);

        velocity = Vector2.ClampMagnitude(velocity + steering, speed);

        lookAt(velocity);
        body.velocity = velocity;
    }



    private void edges()
    {


        if (transform.position.x < minX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        else if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
    }

    private Vector2 flock_force()
    {
        Vector2 v = Vector2.zero;

        int neighborCount = 0;

        Vector2 v_align = new Vector2();

        Vector2 v_cohe = new Vector2();
        Vector2 centerOfMass_cohe = new Vector2();

        Vector2 v_sepa = new Vector2();


        Collider2D[] neightbours = Physics2D.OverlapCircleAll(transform.position, neightBourRadius, selfMask);


        foreach (var triangle in neightbours)
        {


            float d = Vector2.Distance(transform.position, triangle.transform.position);
            if (triangle != this)
            {
                //align
                if (d <= alignRadius)
                {
                    v_align += triangle.GetComponent<Rigidbody2D>().velocity;
                }

                if (d <= coheRadius)
                {
                    //cohe
                    centerOfMass_cohe += (Vector2)triangle.transform.position;
                }

                if (d <= sepaRadius)
                {
                    //sepa
                    v_sepa += (Vector2)triangle.transform.position - (Vector2)transform.position;

                }
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            //align
            v_align = (v_align / neighborCount).normalized;

            centerOfMass_cohe = centerOfMass_cohe / neighborCount;
            v_cohe = (centerOfMass_cohe - (Vector2)transform.position).normalized;

            v_sepa /= neighborCount;
            v_sepa *= -1;
            v_sepa = v_sepa.normalized;

            v = v_align + v_cohe + v_sepa;
        }

        return v;

    }

    private Vector2 alignment()
    {
        Vector2 v = new Vector2();
        int neighborCount = 0;

        Collider2D[] neightbours = Physics2D.OverlapCircleAll(transform.position, neightBourRadius, selfMask);
        foreach (var triangle in neightbours)
        {
            float d = Vector2.Distance(transform.position, triangle.transform.position);
            if (triangle != this
               && d < alignRadius)
            {
                v += triangle.GetComponent<Rigidbody2D>().velocity;
                neighborCount++;
            }
        }

        if (neighborCount > 0)
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
        Collider2D[] neightbours = Physics2D.OverlapCircleAll(transform.position, neightBourRadius, selfMask);
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
        Vector2 v = Vector2.zero;
        int neighborCount = 0;

        Collider2D[] neightbours = Physics2D.OverlapCircleAll(transform.position, neightBourRadius, selfMask);
        foreach (var triangle in neightbours)
        {
            float d = Vector2.Distance(transform.position, triangle.transform.position);

            if (triangle != this
                && d < sepaRadius)
            {
                //Vector2 diff = (Vector2)transform.position - (Vector2)triangle.transform.position;
                //float scale = diff.magnitude / Mathf.Sqrt(sepaRadius);

                //v = diff.normalized / scale;


                v += (Vector2)triangle.transform.position - (Vector2)transform.position;


                neighborCount++;
            }
        }

        if (neighborCount != 0)
        {
            v /= neighborCount;
            v *= -1;
            v = v.normalized * 5;
        }

        return v;
    }

    void mao.ICanDisable.disabled()
    {
        this.enabled = false;
        //this.GetComponent<PolygonCollider2D>().enabled = false;
    }
    void mao.ICanDisable.enabled()
    {
        this.enabled = true;
        //this.GetComponent<PolygonCollider2D>().enabled = true;
    }
}
