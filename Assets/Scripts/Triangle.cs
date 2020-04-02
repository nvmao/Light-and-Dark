using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    protected Rigidbody2D body;
    protected Transform transform;

    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float seek_force = 0.5f;
    protected Vector2 target;
    protected Vector2 velocity = new Vector2(1,1);


    // Start is called before the first frame update
    protected void Start()
    {
        transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        target = Vector2.zero;
    }

    // Update is called once per frame
    protected void Update()
    {
        velocity = body.velocity;
        seek(target);
        lookAt(velocity);

        body.velocity = velocity;
    }

    protected void seek(Vector2 target)
    {
        Vector2 desired = target - (Vector2)transform.position;

        desired = desired.normalized * speed;

        Vector2 steering = desired - velocity;

        steering = Vector2.ClampMagnitude(steering, seek_force);

        velocity = Vector2.ClampMagnitude(velocity + steering, speed);
        
    }

    protected void randomTarget()
    {
        float x = Random.Range(-16, 16);
        float y = Random.Range(-9, 9);
        target = new Vector2(x, y);
    }

    protected void flee(Vector2 target)
    {
        Vector2 desired = target - (Vector2)transform.position;

        desired = desired.normalized * speed;

        Vector2 steering = desired - velocity;

        steering = Vector2.ClampMagnitude(steering, seek_force);

        velocity = Vector2.ClampMagnitude(velocity - steering, speed);

    }

    protected void lookAt(Vector2 target)
    {
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

}
