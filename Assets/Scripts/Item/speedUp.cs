using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mao;

public class speedUp : MonoBehaviour,IOnTouch
{
    [SerializeField]
    float lifeTime = 5f;

    Animator cameraController;

    bool playerPick = false;
    Player player;

    private void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerPick)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
            {
                cameraController.SetBool("isZoomOut", false);
                this.player.resetSpeed();


                player.ItemsList.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void onTouch(Player player)
    {
        cameraController.SetBool("isZoomOut", true);

        GameObject isExist = player.ItemsList.Find(obj => obj.CompareTag("SpeedUp"));
        if (isExist != null)
        {
            player.ItemsList.Remove(isExist);
            Destroy(isExist);
        }
        else
        {
            player.ItemsList.Add(this.gameObject);
        }


        this.player = player;
        player.Speed = 15f;
        player.SeekForce = 4f;

        playerPick = true;
        transform.parent = player.transform;
        transform.localPosition = new Vector2(2f, -2f);

        Destroy(GetComponent<CircleCollider2D>());
    }

}
