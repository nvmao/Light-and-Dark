using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Coin : MonoBehaviour,mao.IOnTouch
{

    private CircleCollider2D collider2D;
    private Light2D light2D;
    private bool lightUp = false;

    [SerializeField] GameObject deathParticle;


    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
        light2D = GetComponentInChildren<Light2D>();
        light2D.pointLightOuterRadius = Random.Range(0.2f,2);
    }

    // Update is called once per frame
    void Update()
    {

        float radius = light2D.pointLightOuterRadius;
        if(lightUp == true)
        {
            radius += Time.deltaTime;
            if(radius >= 2)
            {
                lightUp = false;
            }
        }
        else
        {
            radius -= Time.deltaTime;
            if(radius <= 0.2)
            {
                lightUp = true;
            }
        }

        light2D.pointLightOuterRadius = radius;
    }

    public void onTouch(Player player)
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
