using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject lightGun;

    float time = 7;
    float spawnGunTime = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            time = spawnGunTime;
            if(GameObject.FindGameObjectWithTag("LightGun") == null)
            {
                Instantiate(lightGun, new Vector2(Random.Range(-16, 16), Random.Range(-9, 9)), Quaternion.identity);
            }
        }
    }
}
