using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] GameObject[] sceneButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sceneButtons.Length; i++)
        {
            string scene = "s" + (i + 1).ToString();
            Debug.Log(scene);

            sceneButtons[i].GetComponent<Button>()
                .onClick.AddListener(delegate { MySceneManager.loadScene(scene); });


        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
