using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour,mao.IOnTouch
{
    bool playerPick = false;
    bool foundTarget = false;

    Player player;

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    CircleCollider2D collider;
    [SerializeField]
    CircleCollider2D colliderTarget;

    Transform target;

    [SerializeField] GameObject deathParticle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPick && foundTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position,target.position) < 0.4f)
            {
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                player.ItemsList.Remove(gameObject);

                Destroy(gameObject);
                Destroy(target.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerPick && !foundTarget && collision.CompareTag("DeathlyEnemy"))
        {
            foundTarget = true;
            target = collision.transform;
            transform.parent = null;
        }
    }

    public void onTouch(Player player)
    {
        GameObject isExist = player.ItemsList.Find(obj => obj.CompareTag("Kill"));
        if (isExist != null)
        {
            player.ItemsList.Remove(isExist);
            Destroy(isExist);
        }
        else
        {
            player.ItemsList.Add(this.gameObject);
        }

        playerPick = true;

        this.player = player;

        transform.parent = player.transform;
        transform.localPosition = new Vector2(-2f, -2f);

        Destroy(collider);
    }
}
