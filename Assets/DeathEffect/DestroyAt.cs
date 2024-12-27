 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAt : MonoBehaviour
{
    public float TimeTillDeath;
    public float CurrnetTime;

    // Update is called once per frame
    void Update()
    {
        CurrnetTime += Time.deltaTime;
        if(CurrnetTime >= TimeTillDeath)
        {
            Destroy(this.gameObject);
        }
    }
}
