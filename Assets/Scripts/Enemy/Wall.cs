using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour, mao.IOnTouch
{
    public void onTouch(Player player)
    {
        player.playDeathEffect();
    }
}
