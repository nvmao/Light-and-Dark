using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(-360, 360)));
    }

    // Update is called once per frame
    void Update()
    {
        GameController.instance.TimeLimit -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        GameController.instance.TimeLimit = 30.0f;
    }
}
