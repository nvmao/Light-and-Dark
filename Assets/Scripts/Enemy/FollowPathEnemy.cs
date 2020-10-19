using UnityEngine;
using System.Collections;

public class FollowPathEnemy : Triangle
{
    [SerializeField]
    PathsManager pathsManager;
    int currentDest = 0;

    [SerializeField] LayerMask selfMask;
    // Use this for initialization
    void Start()
    {
        base.Start();
        Debug.Log(pathsManager);
    }

    // Update is called once per frame
    void Update()
    {
        followPath();
        movement();
    }

    protected void movement()
    {
        Vector2 steering = Vector2.zero;

        steering = steering + seek(target);
        steering = steering + separation();

        steering = Vector2.ClampMagnitude(steering, seekForce);

        velocity = Vector2.ClampMagnitude(velocity + steering, speed);

        lookAt(velocity);
        body.velocity = velocity;
    }

    void followPath()
    {
        target = pathsManager.getPath(currentDest).position;
        if(Vector2.Distance(transform.position,target) < 0.3f)
        {
            changePath();
        }
    }

    void changePath()
    {
        currentDest++;
        if(currentDest > pathsManager.length()-1)
        {
            currentDest = 0;
        }
    }

    private Vector2 separation()
    {
        Vector2 v = Vector2.zero;
        int neighborCount = 0;

        Collider2D[] neightbours = Physics2D.OverlapCircleAll(transform.position, 5, selfMask);
        foreach (var triangle in neightbours)
        {
            float d = Vector2.Distance(transform.position, triangle.transform.position);

            if (triangle != this
                && d < 1.2f)
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
            v = v.normalized * 15;
        }

        return v;
    }
}
