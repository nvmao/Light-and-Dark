using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderEnemy : Triangle,mao.IOnTouch
{

    BlurOnAwaken blurOnAwaken;


    private void Awake()
    {
        blurOnAwaken = new BlurOnAwaken(GetComponent<PolygonCollider2D>(),GetComponent<SpriteRenderer>());
        StartCoroutine(blurOnAwaken.wait());
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        randomTarget();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,target) <= 0.2)
        {
            randomTarget();
        }
        movement();
    }

    public void onTouch(Player player)
    {
        player.playDeathEffect();
    }

}
