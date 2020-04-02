using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Triangle
{
    [SerializeField] float radius = 6f;
    
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        base.Start();
        randomTarget();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 playerPosition = FindObjectOfType<Player>().transform.position;


        if (Vector2.Distance(transform.position, playerPosition) <= radius && playerPosition != null)
        {
            speed = 3.5f;
            renderer.color = new Color(255, 0, 181);
            target = playerPosition;
            
        }
        else
        {
            speed = 3;
            renderer.color = new Color(0,0,0);
            if (Vector2.Distance(transform.position, target) <= 0.2)
            {
                randomTarget();
            }
        }



        base.Update();
    }
}
