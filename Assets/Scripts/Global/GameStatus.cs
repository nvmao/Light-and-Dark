using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus 
{
    int coin = 0;
    string coinKey = "coin";

    int bestSurviveTime = 0;
    string bestSurviceTimeKey = "bestSurvice";


    public string CoinKey { get => coinKey; set => coinKey = value; }
    public int Coin { get => coin; set => coin = value; }
    public int BestSurviveTime { get => bestSurviveTime; set => bestSurviveTime = value; }
    public string BestSurviceTimeKey { get => bestSurviceTimeKey; set => bestSurviceTimeKey = value; }
}