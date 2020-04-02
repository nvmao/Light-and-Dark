using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEnemy : Triangle
{
    private SpriteRenderer renderer;
    private float flashTime;

    private enum StateColor { yellow,black};
    private StateColor stateColor;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        stateColor = StateColor.black;
        renderer.color = new Color(0, 0, 0);
        randomFlashtime();
        base.Start();   
    }

    // Update is called once per frame
    void Update()
    {
        flashTime -= Time.deltaTime;
        
        if(flashTime  < 0)
        {
            speed = 3;
            randomFlashtime();
            renderer.color = new Color(0, 0, 0);
            stateColor = StateColor.black;
        }
        else if (flashTime < 3.0f)
        {
            speed = 4;
            renderer.color = new Color(255, 236, 0);
            stateColor = StateColor.yellow;
        }

        if (Vector2.Distance(transform.position, target) <= 0.2)
        {
            randomTarget();
        }
        base.Update();
    }

    void randomFlashtime()
    {
        flashTime = Random.Range(5.0f, 15.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(stateColor == StateColor.black)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<DeathEffect>().exploreCoin(transform.position);
            Destroy(gameObject);
        }
    }

    public bool isYellow()
    {
        return (stateColor == StateColor.yellow);
    }
}
