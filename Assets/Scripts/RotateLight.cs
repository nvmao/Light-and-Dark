using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{
    [SerializeField] float speed = 40;
    float time = 2.0f;
    int rotateRight = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //time -= Time.deltaTime;
        //if(time < 0)
        //{
        //    time = Random.Range(2,15);
        //    rotateRight = rotateRight * -1;
        //}

        GetComponent<Transform>().Rotate(new Vector3(0,0,rotateRight * speed * Time.deltaTime));
    }
}
