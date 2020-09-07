using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus 
{
    int coin = 0;
    string coinKey = "coin";

    public string CoinKey { get => coinKey; set => coinKey = value; }
    public int Coin { get => coin; set => coin = value; }
}