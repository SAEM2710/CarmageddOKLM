using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class S_GameManager : S_GenericSingleton<S_GameManager>
{
    [SerializeField] private int m_iTotalEnemiesCpt;
    //[SerializeField]
    //private float m_fFrequence;
    [SerializeField] private GameObject[] m_goTabAI;
    [SerializeField] private GameObject m_goBoss;
    [SerializeField] private int m_iWaveSpawnBoss;
    [SerializeField] protected AudioClip spawnSound;
    [SerializeField] protected AudioClip[] voicesOnSpawn;

    private int m_iKilledEnemies;
    private bool m_bIsPaused;
    private int m_iWavesCpt;
    private GameObject[] m_goTabSpawns;
    private int m_iCurrentAICpt;
    private bool m_bIsBossSpawned;
    private AudioSource m_asAudio;
    private bool spawnSoundDone = false;
    private bool firstRound = true;
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

    public int iCurrentAICpt
    {
        get
        {
            return m_iCurrentAICpt;
        }
        set
        {
            m_iCurrentAICpt = value;
        }
    }

    void Start()
    {
        m_asAudio = GetComponent<AudioSource>();
        m_bIsBossSpawned = false;
        m_iWavesCpt = 0;
        m_bIsPaused = false;
        m_iKilledEnemies = 0;
        //m_fTime = 0f;

        m_goTabSpawns = GameObject.FindGameObjectsWithTag("Spawn");

        Spawn();
        m_iWavesCpt = 1;
        //m_iCurrentAICpt = GameObject.FindGameObjectsWithTag("AI").Length;
    }

    void Update()
    {
        Pause();

        //Debug.Log(m_iCurrentAICpt);

        if(m_iCurrentAICpt <= 0)
        {
            m_iTotalEnemiesCpt += 10;
            if (m_goTabSpawns.Length > 0)
            {
                Spawn();
            }
            ++m_iWavesCpt;
        }
        if (m_goTabSpawns.Length > 0)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (!m_bIsBossSpawned)
        {
            if (m_iWavesCpt >= m_iWaveSpawnBoss)
            {
                int iRandomSpawn;
                iRandomSpawn = Random.Range(0, m_goTabSpawns.Length);
                m_goBoss.transform.position = m_goTabSpawns[iRandomSpawn].transform.position;
                m_goBoss.transform.rotation = m_goTabSpawns[iRandomSpawn].transform.rotation;
                m_goBoss.SetActive(true);
                m_bIsBossSpawned = true;
            }
        }
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

    private void Spawn()
    {
        int iTotalEnemiesCptBySpawn;
        iTotalEnemiesCptBySpawn = m_iTotalEnemiesCpt / m_goTabSpawns.Length;
        int iCurrentAICptBySpawn = 0;

        for (int i = 0; i < m_goTabSpawns.Length; ++i)
        {
            while (iCurrentAICptBySpawn < iTotalEnemiesCptBySpawn)
            {
                //if (m_fTime > m_fFrequence)
                //{
                    int RandomInt;
                    RandomInt = Random.Range(0, m_goTabAI.Length);
                    GameObject RandomAI;
                    RandomAI = m_goTabAI[RandomInt];

                    //m_fTime = 0f;

                    GameObject goIA;

                    if (!spawnSoundDone)
                    {
                        m_asAudio.PlayOneShot(spawnSound);
                        if (!firstRound)
                        {
                            int number = Random.Range(0, voicesOnSpawn.Length);
                            m_asAudio.PlayOneShot(voicesOnSpawn[number], 5.0f);
                        }
                        
                        spawnSoundDone = true;
                        firstRound = false; 
                    }

                    goIA = Instantiate(RandomAI, m_goTabSpawns[i].transform.position, m_goTabSpawns[i].transform.rotation) as GameObject;
                    ++iCurrentAICptBySpawn;
                    ++m_iCurrentAICpt;
                //}
                //m_fTime += Time.deltaTime;
            }
            
            iCurrentAICptBySpawn = 0;
        }
        spawnSoundDone = false;
    }
}
