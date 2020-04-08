using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Triangle
{
    [SerializeField]
    private GameObject deathParticle;

    float speedUpTime;
    [SerializeField] float speedCanUpTo = 0.4f;

    [SerializeField] bool canSpeedUp = true;
    [SerializeField] bool startSpeedUp = false;

    private Box[] boxs;
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        speedUpTime = speedCanUpTo;
        boxs = FindObjectsOfType<Box>();

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {


        //if (Input.GetMouseButtonDown(0))
        //{
        //    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}

        //if (Vector2.Distance(transform.position, target) <= 0.3f)
        //{
        //    target = velocity.normalized * 20;

        //}

        speedUp();

        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        movement();
    }

    private void movement()
    {
        Vector2 steering = Vector2.zero;

        steering = steering + seek(target);

        steering = Vector2.ClampMagnitude(steering, seek_force);

        velocity = Vector2.ClampMagnitude(velocity + steering, speed);

        lookAt(velocity);
        body.velocity = velocity;
    }



    private void speedUp()
    {
        if (Input.GetMouseButtonDown(1) )
        {
            StartCoroutine(speedUpCoroutine());
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopAllCoroutines();
            resetSpeed();
        }

        if (startSpeedUp)
        {
            speedUpTime -= Time.deltaTime;
            if (speedUpTime < 0)
            {
                StopAllCoroutines();
                resetSpeed();
                speedUpTime = speedCanUpTo;
                canSpeedUp = false;
                startSpeedUp = false;
            }
        }

    }

    private void resetSpeed()
    {
        speed = 6;
        seek_force = 0.8f;
    }

    IEnumerator speedUpCoroutine()
    {
        startSpeedUp = true;
        while (true)
        {
            speed = 15000;
            seek_force = 1.0f;
            yield return null;
        }
    }

    public void playDeathEffect()
    {
        Instantiate(deathParticle, transform.position,Quaternion.identity);
        FindObjectOfType<DeathEffect>().explore(transform.position);
        FindObjectOfType<Menu>().playAnimation();
        

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GoldEnemy") && collision.GetComponent<GoldEnemy>().isYellow())
        {
            return;
        }

        if (collision.CompareTag("Coin") || collision.CompareTag("LightGun") || collision.CompareTag("Door"))
        {
            return;
        }
        playDeathEffect();
    }
}
