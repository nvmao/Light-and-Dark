using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject restartBtn;
    [SerializeField] GameObject ExitBtn;

    void Start()
    {
        //restartBtn.GetComponent<Button>().onClick.AddListener(MySceneManager.reloadScene);
        //ExitBtn.GetComponent<Button>().onClick.AddListener(delegate { MySceneManager.loadScene("map"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
