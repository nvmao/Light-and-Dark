using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isStart = false;

    public static GameController instance;

    GameStatus gameStatus = new GameStatus();

    [SerializeField]
    GameStatusUI gameStatusUI;

    float timeCount;

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

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        DontDestroyOnLoad(gameObject);

        loadData();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        setCoin();
        setBestTime();
    }

    void loadData()
    {
        gameStatus = Database.loadGameStatus(gameStatus);
        gameStatusUI.setAll(gameStatus);
    }
   

    void setCoin()
    {
        gameStatusUI.coin.text = gameStatus.Coin.ToString();
    }

    void setBestTime()
    {
        if (!isStart)
        {
            timeCount = 0;
            return;
        }

        timeCount += Time.deltaTime;

        if(timeCount > gameStatus.BestSurviveTime)
        {
            gameStatus.BestSurviveTime = (int)(timeCount);
            gameStatusUI.bestSurvive.text = gameStatus.BestSurviveTime.ToString();
            Database.saveGameStatus(gameStatus);
        }
    }


    void setBest()
    {

    }


}
