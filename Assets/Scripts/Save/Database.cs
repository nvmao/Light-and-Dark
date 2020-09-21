using UnityEngine;
using System.Collections;

public class Database 
{

    public static GameStatus loadGameStatus(GameStatus gameStatus)
    {
        gameStatus.Coin = PlayerPrefs.GetInt(gameStatus.CoinKey);
        gameStatus.BestSurviveTime = PlayerPrefs.GetInt(gameStatus.BestSurviceTimeKey);

        return gameStatus;
    }
    public static void saveGameStatus(GameStatus gameStatus)
    {

        PlayerPrefs.SetInt(gameStatus.CoinKey, gameStatus.Coin);
        PlayerPrefs.SetInt(gameStatus.BestSurviceTimeKey, gameStatus.BestSurviveTime);
    }
}
