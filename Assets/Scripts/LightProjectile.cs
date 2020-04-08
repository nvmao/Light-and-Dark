using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProjectile : MonoBehaviour
{
    Transform transform;
    Rigidbody2D body;

    [SerializeField]
    float speed = 10;

    GameObject[] targets;
    [SerializeField] GameObject exploreEffect;

    Vector2 target = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();


        targets = GameObject.FindGameObjectsWithTag("Enemy");

        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = findNearTarget().transform.position;

        Vector2 diff = target - (Vector2)transform.position;

        body.velocity = diff.normalized * speed;

        lookAt(body.velocity);

    }

    protected void lookAt(Vector2 target)
    {
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    GameObject findNearTarget()
    {
        GameObject nearTarget = null;
        foreach(var target in targets)
        {
            if(nearTarget == null || Vector2.Distance(target.transform.position,transform.position) 
                < Vector2.Distance(nearTarget.transform.position, transform.position))
            {
                nearTarget = target;
            }
        }
        return nearTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(exploreEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

}
