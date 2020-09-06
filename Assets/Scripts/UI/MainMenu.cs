using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    bool isMenuClick = false;

    [SerializeField]
    Joystick joystick;

    Animator animator;

    bool isPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayed && !GameController.instance.isStart && joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            closeAll();
        }
    }

    public void clickMenuBtn()
    {
        isMenuClick = !isMenuClick;

        animator.SetBool("isClickMenuBtn", isMenuClick);

        AudioManager.instance.play("clicked");
    }

    public void closeAll()
    {
        AudioManager.instance.playBG();

        isPlayed = true;
        GameController.instance.isStart = true;
        animator.SetBool("isCloseAll", true);
    }
}
