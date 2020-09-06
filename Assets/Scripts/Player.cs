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


    List<GameObject> itemsList = new List<GameObject>();

    public List<GameObject> ItemsList { get => itemsList; set => itemsList = value; }

    private void Awake()
    {
        //QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        speedUpTime = speedCanUpTo;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        speedUp();

        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;

        //float maxS = 0.4f;
        //inputX = Mathf.Clamp(inputX, -maxS, maxS);
        //inputY = Mathf.Clamp(inputY, -maxS, maxS);

        //if (Vector2.Distance(transform.position, arrow.position) < 2f)
        //{
        //    float arrowSpeed = speed * Time.deltaTime * 0.8f;
        //    arrow.position = arrow.position + new Vector3(inputX, inputY, 0) * arrowSpeed;

        //}
        if(joystick.Direction.x != 0 && joystick.Direction.y != 0)
        {
            float joyAngle = Mathf.Atan2(inputY, inputX);
            // Convert in degrees
            joyAngle = joyAngle * Mathf.Rad2Deg;

            center.transform.eulerAngles = new Vector3(center.transform.eulerAngles.x, center.transform.eulerAngles.y, joyAngle);

            target = arrow.position;
        }
 

        movement();
    }

    private void movement()
    {
        Vector2 steering = Vector2.zero;

        steering = steering + seek(target);

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
        speed = 10;
        seekForce = 0.92f;
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
        FindObjectOfType<Menu>().playAnimation();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mao.IOnTouch touchObject = null;

        if(collision.transform.parent != null)
        {
           touchObject = collision.transform.parent.GetComponent<mao.IOnTouch>();
        }
        else
        {
            touchObject = collision.transform.GetComponent<mao.IOnTouch>();
        }

        if (touchObject != null)
        {
            touchObject.onTouch(this);
        }
    }
}
