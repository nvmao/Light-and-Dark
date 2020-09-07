﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isStart = false;

    public static GameController instance;

    GameStatus gameStatus = new GameStatus();

    [SerializeField]
    GameStatusUI gameStatusUI;

    public GameStatus GameStatus { get => gameStatus; set => gameStatus = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        loadData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setCoin();
    }

    void loadData()
    {
        gameStatus = Database.loadGameStatus(gameStatus);
    }
   

    void setCoin()
    {
        gameStatusUI.coin.text = gameStatus.Coin.ToString();
    }
    


}
