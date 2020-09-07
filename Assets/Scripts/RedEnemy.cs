using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RedEnemy : MonoBehaviour
{
    private GameObject player;
    private Transform childTranform;

    [SerializeField] private GameObject projectile;


    [SerializeField] float rotateSpeed = 30;

    float shootTime = 1;
    float changeStateTime = 5.0f;
    float delayShooting = 0.15f;
    float delayNormalShooting = 0.5f;

    State state = State.moving;

    bool separate = true;

    Vector2 currentPos = Vector2.zero;
    Vector2 startPos;

    [SerializeField] int life = 30;

    enum State
    {
        shooting,
        shootContinuesly,
        moving,
        moveTo

    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        childTranform = GetComponentsInChildren<Transform>()[1];
        if(childTranform.name != "Triangle")
        {
            childTranform = GetComponentsInChildren<Transform>()[0];
        }

        startPos = transform.position;
        currentPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        

        changeStateTime -= Time.deltaTime;
        if(changeStateTime < 0)
        {
            changeStateTime = 5;
            state = getRandomState();
        }

       if(state == State.shooting)
        {
            normalShooting();
            transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        }
       else if(state == State.shootContinuesly)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
            shootContinuesly();
        }
       else if(state == State.moving){
            transform.Rotate(new Vector3(0, 0, rotateSpeed *5* Time.deltaTime));
            move();
        }
       else if(state == State.moveTo)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed * 5 * Time.deltaTime));
            moveRandom();
        }


    }

    private void moveRandom()
    {
        Vector2 target = currentPos;

        Vector2 dif = target - (Vector2)transform.position;

        dif = dif.normalized;

        transform.position = (Vector2)transform.position + dif * 5.0f * Time.deltaTime;
    }

    private void move()
    {
        if(player == null)
        {
            return;
        }
        Vector2 target = player.transform.position;

        Vector2 dif = target - (Vector2)transform.position;

        dif = dif.normalized;

        transform.position = (Vector2)transform.position + dif * 5.0f * Time.deltaTime;

    }


    private void normalShooting()
    {
        shootTime -= Time.deltaTime;
        if (shootTime < 0)
        {
            //Debug.DrawLine(player.transform.position, child.position,Color.red);

            Instantiate(projectile, childTranform.position, childTranform.rotation);
            shootTime = delayNormalShooting;
        }
    }

    private void shootContinuesly()
    {
        shootTime -= Time.deltaTime;
        if (shootTime < 0)
        {
            //Debug.DrawLine(player.transform.position, child.position,Color.red);

            Instantiate(projectile, childTranform.position, childTranform.rotation);
            shootTime = delayShooting;
        }
    }

    private State getRandomState()
    {
        state++;
        if((int)state > 3)
        {
            state = 0;

            if (separate)
            {
                currentPos = new Vector2(Random.Range(-16, 0), Random.Range(-9, 9));
                GameObject.Find("Global Light 2D").GetComponent<Light2D>().intensity = 0.54f;
            }
            else
            {
                GameObject.Find("Global Light 2D").GetComponent<Light2D>().intensity = 0;
                currentPos = startPos;
            }
            separate = !separate;
        }

        return state;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LightProjectile"))
        {
            life--;
            if(life == 31)
            {
                delayShooting = 0.07f;
                delayNormalShooting = 0.3f;
            }
            if(life < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
