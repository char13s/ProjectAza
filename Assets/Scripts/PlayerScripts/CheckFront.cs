using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class CheckFront : MonoBehaviour
{
    private Player player;
    private float distanceWall;
    // Start is called before the first frame update
    void Start()
    {
        distanceWall = GetComponent<Collider>().bounds.extents.y;
        player = Player.GetPlayer();
    }

    private void FixedUpdate() {
        if (Physics.Raycast(transform.position, player.transform.forward, distanceWall + 0.5f)) {
            player.CantMove = true;
        }
        else {
            player.CantMove = false;
        }
    }
}
