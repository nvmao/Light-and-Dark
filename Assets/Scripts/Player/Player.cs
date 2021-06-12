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

    [SerializeField] Joystick joystick;
    [SerializeField] Transform arrow;
    [SerializeField] Transform center;

    [SerializeField] LayerMask layerMask;

    List<GameObject> itemsList = new List<GameObject>();

    public List<GameObject> ItemsList { get => itemsList; set => itemsList = value; }

    private bool l_left = false;


    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        speedUpTime = speedCanUpTo;
        base.Start();

        StartCoroutine("l_animation");
    }
    // Update is called once per frame
    void Update()
    {
        //if (!GameController.instance.isStart)
        //{
        //    return;
        //}

        //speedUp();

        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //float inputX = joystick.Horizontal;
        //float inputY = joystick.Vertical;


        //if (Vector2.Distance(transform.position, arrow.position) < 2f)
        //{
        //    float arrowSpeed = speed * Time.deltaTime * 0.8f;
        //    arrow.position = arrow.position + new Vector3(inputX, inputY, 0) * arrowSpeed;

        //}
        //if (joystick.Direction.x != 0 && joystick.Direction.y != 0)
        //{
        //    float joyAngle = Mathf.Atan2(inputY, inputX);
        //    // Convert in degrees
        //    joyAngle = joyAngle * Mathf.Rad2Deg;

        //    center.transform.eulerAngles = new Vector3(center.transform.eulerAngles.x, center.transform.eulerAngles.y, joyAngle);

        //    target = arrow.position;
        //}
       
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = mousePosition;
        

        movement();
    }

    IEnumerator l_animation()
    {
        l_left = !l_left;
        yield return new WaitForSeconds(0.01f);
    }

    private void movement()
    {
        Vector2 steering = Vector2.zero;

        steering = steering + seek(target);
        //steering = steering + avoidance(layerMask);

        steering = Vector2.ClampMagnitude(steering, seekForce);

        velocity = Vector2.ClampMagnitude(velocity + steering, speed);

        lookAt(velocity);
        body.velocity = velocity;
    }



    public void speedUp()
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

    public void resetSpeed()
    {
        speed = 13;
        seekForce = 1f;
    }

    IEnumerator speedUpCoroutine()
    {
        startSpeedUp = true;
        while (true)
        {
            speed = 15000;
            seekForce = 1.0f;
            yield return null;
        }
    }

    public void playDeathEffect()
    {
        Instantiate(deathParticle, transform.position,Quaternion.identity);
        FindObjectOfType<DeathEffect>().explore(transform.position);
        GameController.instance.isStart = false;
        body.velocity = new Vector2(0, 0);
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(GetComponent<PolygonCollider2D>());
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        StartCoroutine(reloadScene());
        AudioManager.instance.play("gameOver");
        AudioManager.instance.play("playerDeath");
    }

    IEnumerator reloadScene()
    {
        yield return new WaitForSeconds(2.2f);
        MySceneManager.reloadScene();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mao.IOnTouch touchObject = null;

        if(collision.transform.parent != null)
        {
           touchObject = collision.transform.parent.GetComponent<mao.IOnTouch>();
        }

        if(touchObject == null)
        {
            touchObject = collision.transform.GetComponent<mao.IOnTouch>();
        }

        if (touchObject != null)
        {
            touchObject.onTouch(this);
        }
    }
}
