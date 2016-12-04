using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class S_UIManager : S_GenericSingleton<S_UIManager>
{
    [SerializeField] private Image m_iPauseUI;
    [SerializeField] private Image m_iLifeUI;
    [SerializeField] private Image[] m_iTabShieldUI;
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
        m_iLifeUI.fillAmount = m_pPlayer.fCurrentLife / m_pPlayer.fMaxLife;

        if (m_pPlayer.fCurrentBerzerkValue < m_pPlayer.fMaxBerzerkValue)
        {
            m_cfCameraFilter.Blood_On_Screen = (1f * m_pPlayer.fCurrentBerzerkValue) / m_pPlayer.fMaxBerzerkValue;
        }

        if(m_pPlayer.bShieldActivated)
        {
            switch (m_pPlayer.stShieldType)
            {
                case ShieldType.Poison:
                    m_iTabShieldUI[0].gameObject.SetActive(true);
                    m_iTabShieldUI[0].fillAmount = m_pPlayer.fCurrentShieldValue / m_pPlayer.fMaxShieldValue;

                    m_iTabShieldUI[1].gameObject.SetActive(false);
                    m_iTabShieldUI[2].gameObject.SetActive(false);
                    break;
                case ShieldType.Fire:
                    m_iTabShieldUI[1].gameObject.SetActive(true);
                    m_iTabShieldUI[1].fillAmount = m_pPlayer.fCurrentShieldValue / m_pPlayer.fMaxShieldValue;

                    m_iTabShieldUI[0].gameObject.SetActive(false);
                    m_iTabShieldUI[2].gameObject.SetActive(false);
                    break;
                case ShieldType.Blade:
                    m_iTabShieldUI[2].gameObject.SetActive(true);
                    m_iTabShieldUI[2].fillAmount = m_pPlayer.fCurrentShieldValue / m_pPlayer.fMaxShieldValue;

                    m_iTabShieldUI[0].gameObject.SetActive(false);
                    m_iTabShieldUI[1].gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            for (int i = 0; i < m_iTabShieldUI.Length; ++i)
            {
                m_iTabShieldUI[i].gameObject.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_iLifeUI.fillAmount = m_pPlayer.fCurrentLife / m_pPlayer.fMaxLife;
        if (m_pPlayer.fCurrentBerzerkValue < m_pPlayer.fMaxBerzerkValue)
        {
            m_cfCameraFilter.Blood_On_Screen = (1f * m_pPlayer.fCurrentBerzerkValue) / m_pPlayer.fMaxBerzerkValue;
        }

        m_tKilledEnemiesText.text = "Killed Enemies : " + S_GameManager.Instance.iKilledEnemies.ToString();

        //float fTimer = Time.timeSinceLevelLoad;
        string fMinutes = Mathf.Floor(Time.timeSinceLevelLoad / 60).ToString("00");
        string fSeconds = (Time.timeSinceLevelLoad % 60).ToString("00");
        m_tTimeText.text = fMinutes + ":" + fSeconds;

        if (m_pPlayer.bShieldActivated)
        {
            switch (m_pPlayer.stShieldType)
            {
                case ShieldType.Poison:
                    m_iTabShieldUI[0].gameObject.SetActive(true);
                    m_iTabShieldUI[0].fillAmount = m_pPlayer.fCurrentShieldValue / m_pPlayer.fMaxShieldValue;

                    m_iTabShieldUI[1].gameObject.SetActive(false);
                    m_iTabShieldUI[2].gameObject.SetActive(false);
                    break;
                case ShieldType.Fire:
                    m_iTabShieldUI[1].gameObject.SetActive(true);
                    m_iTabShieldUI[1].fillAmount = m_pPlayer.fCurrentShieldValue / m_pPlayer.fMaxShieldValue;

                    m_iTabShieldUI[0].gameObject.SetActive(false);
                    m_iTabShieldUI[2].gameObject.SetActive(false);
                    break;
                case ShieldType.Blade:
                    m_iTabShieldUI[2].gameObject.SetActive(true);
                    m_iTabShieldUI[2].fillAmount = m_pPlayer.fCurrentShieldValue / m_pPlayer.fMaxShieldValue;

                    m_iTabShieldUI[0].gameObject.SetActive(false);
                    m_iTabShieldUI[1].gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            for (int i = 0; i < m_iTabShieldUI.Length; ++i)
            {
                m_iTabShieldUI[i].gameObject.SetActive(false);
            }
        }
    }
}
