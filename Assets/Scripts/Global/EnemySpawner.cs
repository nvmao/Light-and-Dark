using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject money;
    [SerializeField] GameObject enemy;

    [SerializeField] GameObject[] objects;

    [SerializeField] Player player;

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

    public Vector2 getCurrentMoneyPos()
    {
        if(currentMoney != null)
            return currentMoney.transform.position;

        return Vector2.zero;
    }

    void spawn()
    {
        Vector2 randPosition = new Vector2(Random.Range(minRandX,maxRandX),
            Random.Range(minRandY,maxRandY));
        Vector2 randPosition2 = new Vector2(Random.Range(minRandX, maxRandX),
            Random.Range(minRandY, maxRandY));

        Instantiate(enemy, randPosition * 3f + (Vector2)player.transform.position, Quaternion.identity);
        GameObject newMoney = Instantiate(money, randPosition2 * 3f + (Vector2)player.transform.position, Quaternion.identity);
        currentMoney = newMoney;

        spawnObjects();
    }

    void spawnObjects()
    {
        foreach(GameObject obj in this.objects)
        {
            bool isKill = obj.CompareTag("Kill");
            if (isKill)
            {
                //instanceObject(obj);
                if (Random.Range(1, 3) == 2)
                {
                    instanceObject(obj);
                }
            }
            else if(!isKill)
            {
                instanceObject(obj);
            }
           
        }
    }

    void instanceObject(GameObject obj) 
    {
        Vector2 randPosition = new Vector2(Random.Range(minRandX, maxRandX), Random.Range(minRandY, maxRandY));
        Instantiate(obj, randPosition + (Vector2)player.transform.position, Quaternion.identity);
    }
}
