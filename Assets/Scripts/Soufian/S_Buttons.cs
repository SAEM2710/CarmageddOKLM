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

        SceneManager.LoadScene("Soufian3");
    }

    public void SMenu()
    {
        SceneManager.LoadScene("MenuYu");
    }

    public void SQuit()
    {
        Application.Quit();
    }

    public void Sstart()
    {
        if(Input.GetButtonDown("StartGame"))
        {
            SceneManager.LoadScene("Tuto");
        }
    }

    public void Sexit()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            SQuit();
        }
    }
}
