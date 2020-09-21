using UnityEngine;
using System.Collections;
using TMPro;

[System.Serializable]
public class GameStatusUI 
{
    public TextMeshProUGUI coin;
    public TextMeshProUGUI bestSurvive;


    public void setAll(GameStatus gameStatus)
    {
        coin.text = gameStatus.Coin.ToString();
        bestSurvive.text = gameStatus.BestSurviveTime.ToString();
    }

}
