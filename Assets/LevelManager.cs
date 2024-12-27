using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public Animator CameraAnimator;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ChooseScene(int Scene)
    {
        StartCoroutine(LoadLevel(Scene));
    }

    public IEnumerator LoadLevel(int Scene)
    {
        yield return new WaitForSeconds(1);
        CameraAnimator.SetTrigger("QuitScene");
        yield return new WaitForSeconds(1);
        if (Scene == -2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Scene == -1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        else
        {
            SceneManager.LoadScene(Scene);
        }

    }
}
