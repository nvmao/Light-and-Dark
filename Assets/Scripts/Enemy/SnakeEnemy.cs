using UnityEngine;
using System.Collections;

public class SnakeEnemy : DeathlyEnemy
{
    [SerializeField] int maxParts = 10;
    [SerializeField] SnakePart snakePartObject;

    // Use this for initialization
    void Start()
    {
        base.Start();
        seekForce = 0.1f;
        SnakePart prevPart = null;
        for (int i = 0; i < maxParts; i++)
        {
            SnakePart snakePart = Instantiate(snakePartObject, transform.position, transform.rotation);
            //snakePart.transform.parent = transform;
            snakePart.Speed = this.speed;
            if (i == 0)
            {
                snakePart.FollowTarget = transform;
            }
            else
            {
                snakePart.FollowTarget = prevPart.transform;
            }
            prevPart = snakePart;

        }

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }

}
