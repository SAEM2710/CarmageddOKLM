using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SButtons : MonoBehaviour
{
    public void SPlay()
    {
        if (Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;
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
