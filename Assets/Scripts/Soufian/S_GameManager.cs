using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class S_GameManager : S_GenericSingleton<S_GameManager>
{
    [SerializeField]
    private int m_iTotalEnemiesCpt;
    //[SerializeField]
    //private float m_fFrequence;
    [SerializeField]
    private GameObject[] m_goTabAI;

    private int m_iKilledEnemies;
    private bool m_bIsPaused;
    private int m_iWavesCpt;
    private GameObject[] m_goTabSpawns;
    //private float m_fTime;

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
        //m_fTime = 0f;

        m_goTabSpawns = GameObject.FindGameObjectsWithTag("Spawn");

        Spawn();
    }

    void Update()
    {
        Pause();
        //WavesManager();

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

    /*private void Waves()
    {
        if (GameObject.FindGameObjectsWithTag("AI").Length <= 0)
        {
            ++WavesCpt;
        }
    }

    private void */

    private void Spawn()
    {
        /*switch(m_iWavesCpt)
        {
            case 0:
                Debug.Log("Wave " + m_iWavesCpt);
                /*m_goTabSpawns[0].gameObject.SetActive(true);
                m_goTabSpawns[1].gameObject.SetActive(true);
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
        //vague 3*/

        int iTotalEnemiesCptBySpawn;
        iTotalEnemiesCptBySpawn = m_iTotalEnemiesCpt / m_goTabSpawns.Length;
        //Debug.Log(iTotalEnemiesCptBySpawn);
        int iCurrentAICpt = 0;

        for (int i = 0; i < m_goTabSpawns.Length; ++i)
        {
            while (iCurrentAICpt < iTotalEnemiesCptBySpawn)
            {
                //if (m_fTime > m_fFrequence)
                //{
                    int RandomInt;
                    RandomInt = Random.Range(0, m_goTabAI.Length);
                    GameObject RandomAI;
                    RandomAI = m_goTabAI[RandomInt];

                    //m_fTime = 0f;

                    GameObject goIA;
                    goIA = Instantiate(RandomAI, m_goTabSpawns[i].transform.position, m_goTabSpawns[i].transform.rotation) as GameObject;
                    ++iCurrentAICpt;
                //}
                //m_fTime += Time.deltaTime;
            }
            iCurrentAICpt = 0;
        }
    }
}
