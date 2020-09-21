using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;

public class AutoCollect : MonoBehaviour,mao.IOnTouch
{
    bool playerPick = false;

    [SerializeField]
    CircleCollider2D collider;

    CircleCollider2D rangeCollider;

    Player player;

    [SerializeField]
    float timeLife = 5f;

    [SerializeField] Light2D light;

    float speed = 20f;

    // Use this for initialization
    void Start()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPick)
        {
            timeLife -= Time.deltaTime;
            if(timeLife < 0)
            {
                player.ItemsList.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        //if (playerPick && foundTarget)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        //    if (Vector2.Distance(transform.position, target.position) < 0.4f)
        //    {
        //        Instantiate(deathParticle, transform.position, Quaternion.identity);
        //        

        //        AudioManager.instance.play("playerDeath");
        //       
        //    }
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerPick && collision.CompareTag("Coin"))
        {
            collision.transform.position = Vector2.MoveTowards(collision.transform.position, transform.position, speed * Time.deltaTime);
        }
    }

    public void onTouch(Player player)
    {
        GameObject isExist = player.ItemsList.Find(obj => obj.CompareTag("AutoCollect"));
        if (isExist != null)
        {
            player.ItemsList.Remove(isExist);
            Destroy(isExist);
        }
        else
        {
            player.ItemsList.Add(this.gameObject);
        }

        rangeCollider.enabled = true;
        playerPick = true;

        this.player = player;

        transform.parent = player.transform;
        transform.localPosition = new Vector2(0, 0);

        Destroy(collider);

        //light.pointLightInnerRadius = 14;
        //light.pointLightOuterRadius = 17;

        //AudioManager.instance.play("eatKill");
    }
}
