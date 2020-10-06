using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : Reminant
{
    private Player player;
    [SerializeField] private float move;
    public override void Start()
    {
        player = Player.GetPlayer();
        transform.rotation = new Quaternion(0, -180, 0, 0);
        base.Start();
    }

    void Update()
    {
        transform.position += player.transform.right * move * Time.deltaTime;
    }
}
