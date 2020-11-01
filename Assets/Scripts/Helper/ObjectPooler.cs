using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }


    [SerializeField]
    List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Use this for initialization
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    public GameObject spawn(string tag,Vector3 position,Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("no tag found in pool: " + tag);
            return null;
        }

        if(poolDictionary[tag].Count == 0)
        {
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        mao.IOnStartPool haveOnstart = obj.GetComponent<mao.IOnStartPool>();
        if(haveOnstart != null)
        {
            haveOnstart.onStart();
        }

        poolDictionary[tag].Enqueue(obj);

        return obj;
    }

    

}
