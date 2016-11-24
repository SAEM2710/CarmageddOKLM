using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SUIManager : GenericSingleton<SGameManager>
{
    #region Visible Variables

    [SerializeField] private Image m_iPauseUI;
    [SerializeField] private Slider m_sLifeUI;
    [SerializeField] private Text m_tKilledEnemiesText;
    [SerializeField] private Text m_tTimeText;

    #endregion

    private SPlayer m_spPlayer;
    private bool m_bIsPaused;

    // Use this for initialization
    void Start ()
    {
        m_bIsPaused = false;
        m_iPauseUI.gameObject.SetActive(false);
        m_spPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
        m_sLifeUI.value = (m_sLifeUI.maxValue * m_spPlayer.fCurrentLife)/
                                                m_spPlayer.fMaxLife;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Pause();
        m_sLifeUI.value = (m_sLifeUI.maxValue * m_spPlayer.fCurrentLife) /
                                        m_spPlayer.fMaxLife;

        m_tKilledEnemiesText.text = "Killed Enemies : " + SGameManager.Instance.iKilledEnemies.ToString();
        m_tTimeText.text = Time.timeSinceLevelLoad.ToString();
    }

    void Pause()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (m_bIsPaused)
            {
                m_bIsPaused = false;
                m_iPauseUI.gameObject.SetActive(false);
                AudioListener.pause = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                m_bIsPaused = true;
                m_iPauseUI.gameObject.SetActive(true);
                AudioListener.pause = true;
                Time.timeScale = 0.0f;
            }
        }
    }
}
