using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightGun : MonoBehaviour
{
    private CircleCollider2D collider2D;
    private Light2D light2D;
    private bool lightUp = false;
    private bool haveParrent = false;

    private float time = 0.2f;
    private float delayShootTime = 0.2f;

    [SerializeField] GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
        light2D = GetComponentInChildren<Light2D>();
        light2D.pointLightOuterRadius = Random.Range(0.2f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (haveParrent)
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                time = delayShootTime;
                Instantiate(projectile, transform.position, transform.rotation);
            }
        }

        LightTime();
    }



    private void LightTime()
    {
        float radius = light2D.pointLightOuterRadius;
        if (lightUp == true)
        {
            radius += Time.deltaTime;
            if (radius >= 2)
            {
                lightUp = false;
            }
        }
        else
        {
            radius -= Time.deltaTime;
            if (radius <= 0.2)
            {
                lightUp = true;
            }
        }

        light2D.pointLightOuterRadius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.parent != null)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent = collision.gameObject.transform;
            transform.position = collision.gameObject.transform.position + new Vector3(0.1f, 0.5f, 0);
            transform.localScale = new Vector3(0.5f, 0.5f, 0);
            haveParrent = true;
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(gameObject, 5.0f);
        }
    }
}
