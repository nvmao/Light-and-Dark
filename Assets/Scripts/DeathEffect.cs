using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{

    [SerializeField] GameObject smallLight;
    [SerializeField] GameObject coin;

    // Start is called before the first frame update

    public void exploreCoin(Vector2 position)
    {
        for (int i = 0; i < 15; i++)
        {
            ObjectPooler.Instance.spawn("MovingCoin", position, Quaternion.identity);
        }
    }

    public void explore(Vector2 position)
    {
        for(int i=0;i < 50; i++)
        {
            Instantiate(smallLight,position,Quaternion.identity);
        }
    }

}
