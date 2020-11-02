using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : Triangle,mao.IOnStartPool
{
    BlurOnAwaken blurOnAwaken;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        randomTarget();
        speed = Random.Range(1, 7);

        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if(collider != null)
        {
            StartCoroutine(disableCollider(collider));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector2.Distance(transform.position,target) <= 0.1)
        {
            speed = 0;
        }

        movement();
    }

    IEnumerator disableCollider(CircleCollider2D collider)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(1.0f);
        collider.enabled = true;
    }

    public void onStart()
    {
        this.Start();

        blurOnAwaken = new BlurOnAwaken(this,GetComponent<CircleCollider2D>(), GetComponent<SpriteRenderer>());
        StartCoroutine(blurOnAwaken.wait());
    }
}
