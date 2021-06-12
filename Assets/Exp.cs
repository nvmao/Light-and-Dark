using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour, mao.IOnTouch
{

    [SerializeField] GameObject deathParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onTouch(Player player)
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        AudioManager.instance.play("coinDeath");

        gameObject.SetActive(false);
    }
}
