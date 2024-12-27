using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Foot : MonoBehaviour
{
    public Player p;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        p.Grounded = true;
        if (collision.gameObject.tag == "SpreadTile")
        {
            p.SpreadTile(collision.gameObject.GetComponent<Tilemap>());
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        p.Grounded = true;
        if (collision.gameObject.tag == "SpreadTile")
        {
            p.SpreadTile(collision.gameObject.GetComponent<Tilemap>());
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        p.Grounded = false;
    }
}
