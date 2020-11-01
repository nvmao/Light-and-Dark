using UnityEngine;
using System.Collections;

public class SnakeEnemy : Triangle,mao.IOnTouch
{

    [SerializeField] Transform followTarget;

    bool targetDeath = false;

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if(followTarget != null)
        {
            this.target = followTarget.position;
            targetDeath = true;
        }
        else if(targetDeath)
        {
            //followTarget = FindObjectOfType<Player>().transform;
            randomTarget();
            targetDeath = false;
        }
        arrive(target, 2);

    }

    public void onTouch(Player player)
    {
        player.playDeathEffect();
    }
}
