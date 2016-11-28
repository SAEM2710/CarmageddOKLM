using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class S_GameManager : S_GenericSingleton<S_GameManager>
{
    private int m_iKilledEnemies;
    private bool m_bIsPaused;
    private int m_iWavesCpt;
    private int m_iTotalEnemiesCpt;
    private GameObject[] m_goTabSpawns;

    public int iKilledEnemies
    {
        get
        {
            return m_iKilledEnemies;
        }
        set
        {
            m_iKilledEnemies = value;
        }
    }

    void Start()
    {
        m_iWavesCpt = 0;
        m_bIsPaused = false;
        m_iKilledEnemies = 0;

        m_goTabSpawns = GameObject.FindGameObjectsWithTag("Spawn");

        for (int i = 0; i < m_goTabSpawns.Length; ++i)
        {
            m_iTotalEnemiesCpt += m_goTabSpawns[i].GetComponent<S_Spawn>().iMaxCptAI;
            Debug.Log(m_goTabSpawns[i]);
        }
    }

    void Update()
    {
        Pause();
        WavesManager();
    }

    void Pause()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (m_bIsPaused)
            {
                m_bIsPaused = false;
                S_UIManager.Instance.iPauseUI.gameObject.SetActive(false);
                AudioListener.pause = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                m_bIsPaused = true;
                S_UIManager.Instance.iPauseUI.gameObject.SetActive(true);
                AudioListener.pause = true;
                Time.timeScale = 0.0f;
            }
        }
    }

    private void WavesManager()
    {
        switch(m_iWavesCpt)
        {
            case 0:
                Debug.Log("Wave " + m_iWavesCpt);
                /*m_goTabSpawns[0].gameObject.SetActive(true);
                m_goTabSpawns[1].gameObject.SetActive(true);*/
                break;
            case 1:
                Debug.Log("Wave " + m_iWavesCpt);
                break;
            case 2:
                Debug.Log("Wave " + m_iWavesCpt);
                break;
        }
        //m_goTabSpawns
        //vague 1, instantier 40 ennemis, 20 en haut, 20 en bas
        // vague 2, instantier 60 ennemis, 20 en haut, 20 en bas, 20 a droite
        //vague 3
    }

}
