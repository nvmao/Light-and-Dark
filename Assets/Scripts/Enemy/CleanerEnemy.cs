using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerEnemy : Triangle,mao.IOnTouch
{
    [SerializeField] float rotateSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        rotating();
    }

    void rotating()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }

    public void onTouch(Player player)
    {
        player.playDeathEffect();
    }
}
