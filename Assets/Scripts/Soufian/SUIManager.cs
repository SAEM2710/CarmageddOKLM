using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SUIManager : GenericSingleton<SUIManager>
{
    #region Visible Variables

    [SerializeField] private Image m_iPauseUI;
    [SerializeField] private Slider m_sLifeUI;
    [SerializeField] private Text m_tKilledEnemiesText;
    [SerializeField] private Text m_tTimeText;

    #endregion

    private SPlayer m_spPlayer;

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
        m_iPauseUI.gameObject.SetActive(false);
        m_spPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SPlayer>();
        m_sLifeUI.value = (m_sLifeUI.maxValue * m_spPlayer.fCurrentLife)/
                                                m_spPlayer.fMaxLife;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_sLifeUI.value = (m_sLifeUI.maxValue * m_spPlayer.fCurrentLife) /
                                        m_spPlayer.fMaxLife;

        m_tKilledEnemiesText.text = "Killed Enemies : " + SGameManager.Instance.iKilledEnemies.ToString();
        m_tTimeText.text = Time.timeSinceLevelLoad.ToString();
    }


}
