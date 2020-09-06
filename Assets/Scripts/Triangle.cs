using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    protected Rigidbody2D body;

    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float seekForce = 0.5f;
    protected Vector2 target;
    protected Vector2 velocity = new Vector2(1,1);

    public float Speed { get => speed; set => speed = value; }
    public float SeekForce { get => seekForce; set => seekForce = value; }


    // Start is called before the first frame update
    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = Vector2.zero;
    }

    // Update is called once per frame
    protected void Update()
    {
       
    }

    protected void movement()
    {
        Vector2 steering = Vector2.zero;

        steering = steering + seek(target);

        steering = Vector2.ClampMagnitude(steering, seekForce);

        velocity = Vector2.ClampMagnitude(velocity + steering, speed);

        lookAt(velocity);
        body.velocity = velocity;
    }

    protected Vector2 seek(Vector2 target)
    {
        velocity = body.velocity;

        Vector2 desired = target - (Vector2)transform.position;

        desired = desired.normalized * speed;

        Vector2 steering = desired - velocity;

        return steering;
    }

    protected void arrive(Vector2 target, float slowingRadius)
    {
        velocity = body.velocity;
        Vector2 desired = target - (Vector2)transform.position;
        float distance = Vector2.Distance(transform.position, target);

        if (distance < slowingRadius)
        {
            desired = desired.normalized * speed * (distance / slowingRadius);
        }
        else 
        {
            desired = desired.normalized * speed;
        }
        Vector2 steering = desired - velocity;
        velocity = Vector2.ClampMagnitude(velocity + steering, speed);
        lookAt(velocity);

        body.velocity = velocity;
    }

    protected void randomTargetOnscreen()
    {
        float minRandX = -40;
        float maxRandX = 130 ;
        float minRandY = -90;
        float maxRandY = 50;

        target = new Vector2(Random.Range(minRandX, maxRandX), Random.Range(minRandY, maxRandY));

    }
    protected void randomTarget()
    {
        float minRandX = -20;
        float maxRandX = 20;
        float minRandY = -20;
        float maxRandY = 20;

        target = new Vector2(Random.Range(minRandX, maxRandX), Random.Range(minRandY, maxRandY));

        target = ((Vector2)transform.position) + target * 3f;
    }

    protected Vector2 flee(Vector2 target)
    {
        Vector2 desired = target - (Vector2)transform.position;

        desired = desired.normalized * speed;

        Vector2 steering = desired - velocity;

        return steering;

    }

    protected void lookAt(Vector2 target)
    {
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

     protected Vector2 avoidance(Box[] boxs)
    {
        float dynamic_length = velocity.magnitude / speed;


        Vector2 ahead = (Vector2)transform.position + velocity.normalized * dynamic_length;
        Vector2 ahead2 = (Vector2)transform.position + velocity.normalized * dynamic_length * 0.5f;


        Box box = findMostThreateningBox(ahead, ahead2,boxs);

        Vector2 avoidance = Vector2.zero;

        if (box != null)
        {
            avoidance = ahead - (Vector2)box.transform.position;

            avoidance = avoidance.normalized * speed * 2;

            //avoidance = Vector2.ClampMagnitude(avoidance, 20f);

        }
        return avoidance;
    }

    private Box findMostThreateningBox(Vector2 ahead, Vector2 ahead2,Box[] boxs)
    {
        Box mostThreatening = null;

        foreach (var box in boxs)
        {

            float boxRadius = (box.GetComponent<Transform>().localScale.x / 2) * 1.2f;
            float d_ahead = Vector2.Distance(box.transform.position, ahead);
            float d_ahead2 = Vector2.Distance(box.transform.position, ahead2);

            bool collision = (d_ahead <= boxRadius || d_ahead2 <= boxRadius);

            float d_to_box = Vector2.Distance(transform.position, box.transform.position);
            Debug.Log(collision);
            if (collision && (mostThreatening == null || d_to_box <
                Vector2.Distance(transform.position, mostThreatening.transform.position)))
            {
                mostThreatening = box;
            }
        }
        return mostThreatening;
    }
}
