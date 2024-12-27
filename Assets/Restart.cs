using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public AudioClip burstSound;
    public float ErosionFrequency;
    public float ErosionTime;
    public GameObject EnergyCircle;
    public AudioSource source;
    private void Update()
    {
        ErosionTime += Time.deltaTime;
        if (ErosionTime >= ErosionFrequency)
        {
            source.PlayOneShot(burstSound);
            Instantiate(EnergyCircle);
            ErosionTime -= ErosionFrequency;
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
