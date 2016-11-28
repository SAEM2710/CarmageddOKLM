using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_UIManager : S_GenericSingleton<S_UIManager>
{
    [SerializeField] private Image m_iPauseUI;
    [SerializeField] private Slider m_sLifeUI;
    [SerializeField] private Text m_tKilledEnemiesText;
    [SerializeField] private Text m_tTimeText;

    private S_Player m_pPlayer;

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
        m_pPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<S_Player>();
        m_sLifeUI.value = (m_sLifeUI.maxValue * m_pPlayer.fCurrentLife)/
                                                m_pPlayer.fMaxLife;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_sLifeUI.value = (m_sLifeUI.maxValue * m_pPlayer.fCurrentLife) /
                                        m_pPlayer.fMaxLife;

        m_tKilledEnemiesText.text = "Killed Enemies : " + S_GameManager.Instance.iKilledEnemies.ToString();
        m_tTimeText.text = Time.timeSinceLevelLoad.ToString();
    }
}
