using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mao;

public class speedUp : MonoBehaviour,IOnTouch
{
    [SerializeField]
    float lifeTime = 5f;

    bool playerPick = false;
    Player player;

    private void Update()
    {
        if (playerPick)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
            {
                this.player.resetSpeed();
                Destroy(gameObject);
            }
        }
        
    }

    public void onTouch(Player player)
    {
        this.player = player;
        player.Speed = 15f;
        player.SeekForce = 4f;

        playerPick = true;
        transform.parent = player.transform;
        transform.localPosition = new Vector2(1, 0);

        Destroy(GetComponent<CircleCollider2D>());
    }

}
