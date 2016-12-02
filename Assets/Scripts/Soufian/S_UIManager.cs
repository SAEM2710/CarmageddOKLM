using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class S_UIManager : S_GenericSingleton<S_UIManager>
{
    [SerializeField] private Image m_iPauseUI;
    [SerializeField] private Slider m_sLifeUI;
    [SerializeField] private Slider m_sShieldUI;
    [SerializeField] private Text m_tKilledEnemiesText;
    [SerializeField] private Text m_tTimeText;

    private S_Player m_pPlayer;
    private CameraFilterPack_AAA_BloodOnScreen m_cfCameraFilter;

    #region Getters/Setters

    public Image iPauseUI
    {
        get
        {
            return m_iPauseUI;
        }
        set
        {
            m_iPauseUI = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start ()
    {
        m_cfCameraFilter = Camera.main.GetComponent<CameraFilterPack_AAA_BloodOnScreen>();
        m_iPauseUI.gameObject.SetActive(false);
        m_pPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<S_Player>();
        m_sLifeUI.value = (m_sLifeUI.maxValue * m_pPlayer.fCurrentLife)/
                                                m_pPlayer.fMaxLife;

        m_cfCameraFilter.Blood_On_Screen = (1f * m_pPlayer.fCurrentBerzerkValue) / m_pPlayer.fMaxBerzerkValue;

        if(m_pPlayer.bShieldActivated)
        {
            m_sShieldUI.gameObject.SetActive(true);
        }
        else
        {
            m_sShieldUI.gameObject.SetActive(false);
        }

        m_sShieldUI.value = (m_sShieldUI.maxValue * m_pPlayer.fCurrentShieldValue) /
                                        m_pPlayer.fMaxShieldValue;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_sLifeUI.value = (m_sLifeUI.maxValue * m_pPlayer.fCurrentLife) / m_pPlayer.fMaxLife;
        m_cfCameraFilter.Blood_On_Screen = (1f * m_pPlayer.fCurrentBerzerkValue) / m_pPlayer.fMaxBerzerkValue;

        m_tKilledEnemiesText.text = "Killed Enemies : " + S_GameManager.Instance.iKilledEnemies.ToString();

        //float fTimer = Time.timeSinceLevelLoad;
        string fMinutes = Mathf.Floor(Time.timeSinceLevelLoad / 60).ToString("00");
        string fSeconds = (Time.timeSinceLevelLoad % 60).ToString("00");
        m_tTimeText.text = fMinutes + ":" + fSeconds;

        if (m_pPlayer.bShieldActivated)
        {
            m_sShieldUI.gameObject.SetActive(true);
        }
        else
        {
            m_sShieldUI.gameObject.SetActive(false);
        }

        m_sShieldUI.value = (m_sShieldUI.maxValue * m_pPlayer.fCurrentShieldValue) / m_pPlayer.fMaxShieldValue;
    }
}
