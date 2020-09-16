using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    public GameObject player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.GetComponent<Player>().GroundedOn();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player.GetComponent<Player>().GroundedOff();
    }
}
