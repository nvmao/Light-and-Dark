using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEnemy : Triangle,mao.IOnTouch
{
    private SpriteRenderer renderer;
    private float flashTime;

    private enum StateColor { yellow,black};
    private StateColor stateColor;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        renderer = GetComponent<SpriteRenderer>();
        stateColor = StateColor.black;

        animator.enabled = false;
        renderer.color = new Color(0, 0, 0);

        randomFlashtime();
        base.Start();   
    }

    // Update is called once per frame
    void Update()
    {
        flashTime -= Time.deltaTime;

        if (flashTime < 0)
        {
            animator.enabled = false;
            animator.SetBool("isStateChange", false);

            speed = 3;
            randomFlashtime();
            renderer.color = new Color(0, 0, 0);
            stateColor = StateColor.black;
        }
        else if(flashTime < 1f)
        {
            animator.enabled = true;
            animator.SetBool("isStateChange", true);

        }
        else if (flashTime < 6.0f)
        {
            animator.enabled = false;
            animator.SetBool("isStateChange", false);
            speed = 4;
            renderer.color = new Color(255, 236, 0);
            stateColor = StateColor.yellow;
        }
        else if (flashTime < 7f)
        {
            animator.enabled = true;
            animator.SetBool("isStateChange", true);
        }


        if (Vector2.Distance(transform.position, target) <= 0.2)
        {
            randomTarget();
        }
        movement();
    }

    void randomFlashtime()
    {
        flashTime = Random.Range(8.0f, 15.0f);
    }


    public bool isYellow()
    {
        return (stateColor == StateColor.yellow);
    }

    public void onTouch(Player player)
    {
        if (stateColor == StateColor.black)
        {
            player.playDeathEffect();
            return;
        }
        FindObjectOfType<DeathEffect>().exploreCoin(transform.position);
        Destroy(gameObject);
    }
}
