using System.Collections;
using System.Collections.Generic;
using mao;
using UnityEngine;

public class DeathlyEnemy : Triangle,mao.IOnTouch,mao.IOnStartPool
{
    [SerializeField]
    protected GameObject player;
    private float timeFollow = 5.0f;
    private float timeWait = 0.8f;
    private float timeChangePos = 0.5f;

    [SerializeField]
    protected GameObject deathEffect;

    [SerializeField] LayerMask layerMask;

    protected BlurOnAwaken blurOnAwaken;

    private void Awake()
    {
        //onStart();
    }

    public void onStart()
    {
        blurOnAwaken = new BlurOnAwaken(this,GetComponent<PolygonCollider2D>(),GetComponent<SpriteRenderer>(),null,true);
        StartCoroutine(blurOnAwaken.wait());
        this.Start();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        //speed = Random.Range(8, 18);
        seekForce = Random.Range(0.1f, 1.8f);
        randomTarget();
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        chasing();
        movement();
    }

    private void movement()
    {
        Vector2 steering = Vector2.zero;

        steering = steering + seek(target);

        steering = steering + avoidance(layerMask);

        steering = Vector2.ClampMagnitude(steering, seekForce);

        velocity = Vector2.ClampMagnitude(velocity + steering, speed);

        lookAt(velocity);
        body.velocity = velocity;
    }


    private void chasing()
    {
        if(player == null)
        {
            return;
        }
        if (timeFollow > 0)
        {
            timeFollow -= Time.deltaTime;
            timeChangePos -= Time.deltaTime;
            if (timeChangePos < 0)
            {
                timeChangePos = Random.Range(0.1f, 0.5f);
                target = player.transform.position;
            }
            if (timeFollow <= 0 || Vector2.Distance(transform.position, target) < 1f)
            {
                timeFollow = 0;
                Vector2 direction = target - (Vector2)transform.position;
                target = (Vector2)transform.position + direction.normalized * 20.0f;
            }
        }

        if (timeFollow <= 0)
        {

            timeWait -= Time.deltaTime;
            if (timeWait < 0)
            {
                timeWait = Random.Range(0.6f, 2.0f);
                timeFollow = 5.0f;
            }
        }
    }

    public void onTouch(Player player)
    {
        player.playDeathEffect();
    }

    protected virtual void touchSelf(Collider2D collision)
    {
        if (collision.CompareTag("DeathlyEnemy"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            AudioManager.instance.play("playerDeath");
            gameObject.SetActive(false);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //touchSelf(collision);
    }
   

}
