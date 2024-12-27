using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            this.gameObject.transform.parent.gameObject.GetComponent<Player>().Die();
        }
        if (collision.gameObject.tag == "Goal")
        {
            this.gameObject.transform.parent.gameObject.GetComponent<Player>().NextLevle();
        }
    }
}
