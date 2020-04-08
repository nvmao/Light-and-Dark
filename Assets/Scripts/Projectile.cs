using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Transform transform;
    Rigidbody2D body;

    [SerializeField]
    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = transform.up * speed;
    }
}
