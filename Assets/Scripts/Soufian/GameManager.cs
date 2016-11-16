using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Visible Variables

    [SerializeField] private Image m_iPauseSprite;

    #endregion

    private bool m_bIsPaused;

    // Use this for initialization
    void Start()
    {
        m_bIsPaused = false;
        m_iPauseSprite.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    void Pause()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (m_bIsPaused)
            {
                m_bIsPaused = false;
                m_iPauseSprite.gameObject.SetActive(false);
                AudioListener.pause = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                m_bIsPaused = true;
                m_iPauseSprite.gameObject.SetActive(true);
                AudioListener.pause = true;
                Time.timeScale = 0.0f;
            }
        }
    }
}
