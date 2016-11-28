using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class S_Buttons : MonoBehaviour
{
    public void SPlay()
    {
        if (Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;
        }
        if(AudioListener.pause)
        {
            AudioListener.pause = false;
        }

        SceneManager.LoadScene("Soufian2");
    }

    public void SMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SQuit()
    {
        Application.Quit();
    }
}
