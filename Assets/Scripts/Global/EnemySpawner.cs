using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject money;
    [SerializeField] GameObject enemy;

    GameObject currentMoney;

    [SerializeField]
    float minRandX=0,maxRandX = 0;
    [SerializeField]
    float minRandY = 0, maxRandY = 0;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 randPosition = new Vector2(Random.Range(minRandX, maxRandX), Random.Range(minRandY, maxRandY));
        GameObject newMoney = Instantiate(money, randPosition, Quaternion.identity);
        currentMoney = newMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMoney == null)
        {
            spawn();
        }


    }

    void spawn()
    {
        Vector2 randPosition = new Vector2(Random.Range(minRandX, maxRandX), Random.Range(minRandY, maxRandY));
        Vector2 randPosition2 = new Vector2(Random.Range(minRandX, maxRandX), Random.Range(minRandY, maxRandY));
        Instantiate(enemy, randPosition2, Quaternion.identity);
        GameObject newMoney = Instantiate(money, randPosition, Quaternion.identity);
        currentMoney = newMoney;

    }
}
