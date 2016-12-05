using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class S_Buttons : MonoBehaviour
{
    void Update()
    {
        Sstart();
        Sexit();
        Sskip();
        SgoToMenu();

    }
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
        if(Input.GetButtonDown("StartGame") && SceneManager.GetActiveScene().name == "MenuYu")
        {
            SceneManager.LoadScene("Tuto");
        }
    }

    public void Sexit()
    {
        if(Input.GetButtonDown("Cancel") && SceneManager.GetActiveScene().name == "MenuYu")
        {
            SQuit();
        }
    }

    public void Sskip()
    {
        if (Input.GetButtonDown("StartGame") && SceneManager.GetActiveScene().name == "Tuto")
        {
            SceneManager.LoadScene("soufian3");
        }
    }

    public void SgoToMenu()
    {
        if (Input.GetButtonDown("StartGame") && SceneManager.GetActiveScene().name == "S_GameOver")
        {
            SceneManager.LoadScene("MenuYu");
        }
        if (AudioListener.pause && Input.GetButtonDown("StartGame") && SceneManager.GetActiveScene().name == "soufian3")
        {
            AudioListener.pause = false;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("MenuYu");
        }
    }
}
