using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool hasSpawn = true;

    [SerializeField] GameObject money;
    [SerializeField] GameObject enemy;

    [SerializeField] GameObject[] objects;

    [SerializeField] Player player;

    [SerializeField] GameObject lineDoor;
    GameObject currentLineDoor;

    GameObject currentMoney;

    [SerializeField]
    float minRandX=0,maxRandX = 0;
    [SerializeField]
    float minRandY = 0, maxRandY = 0;

    float spawnTime = 5f;
    float spawnTimeCount;
    



    // Start is called before the first frame update
    void Start()
    {
        spawnTimeCount = spawnTime;
        Vector2 randPosition = new Vector2(Random.Range(minRandX, maxRandX), Random.Range(minRandY, maxRandY));
        GameObject newMoney = Instantiate(money, randPosition, Quaternion.identity);
        currentMoney = newMoney;
    }

    // Update is called once per frame
    void Update()
    {
        //if(currentMoney == null)
        //{
        //    spawn();
        //}

        if (hasSpawn)
        {
            spawnTimeCount -= Time.deltaTime;
            if (spawnTimeCount < 0)
            {
                spawn();
                spawnTimeCount = spawnTime;
            }
        }

        spawnLineDoor();

    }

    public void spawnLineDoor()
    {
        if(currentLineDoor != null)
        {
            return;
        }
        Vector2 randPosition = new Vector2(Random.Range(minRandX, maxRandX), Random.Range(minRandY, maxRandY));
        currentLineDoor = Instantiate(lineDoor, randPosition + (Vector2)player.transform.position, Quaternion.identity);
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
        Vector2 randPosition3 = new Vector2(Random.Range(minRandX, maxRandX),
            Random.Range(minRandY, maxRandY));

        ObjectPooler.Instance.spawn("DeathlyEnemy", randPosition * 3f + (Vector2)player.transform.position, Quaternion.identity);
        ObjectPooler.Instance.spawn("DeathlyEnemy", randPosition3 * 3f + (Vector2)player.transform.position, Quaternion.identity);

        GameObject newMoney = Instantiate(money, randPosition2 * 3f + (Vector2)player.transform.position, Quaternion.identity);
        currentMoney = newMoney;

        spawnObjects();
    }

    void spawnObjects()
    {
        foreach(GameObject obj in this.objects)
        {
            if (obj.CompareTag("Kill"))
            {
                //instanceObject(obj);
                if (Random.Range(1, 3) == 2)
                {
                    instanceObject(obj);
                }
            }
            else if (obj.CompareTag("AutoCollect"))
            {
                if (Random.Range(1, 50) == 2)
                {
                    instanceObject(obj);
                }
            }
            else
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
