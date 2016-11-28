﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SGameManager : GenericSingleton<SGameManager>
{
    private int m_iKilledEnemies;
    private bool m_bIsPaused;

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
        m_bIsPaused = false;
        m_iKilledEnemies = 0;
    }

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
                SUIManager.Instance.iPauseUI.gameObject.SetActive(false);
                AudioListener.pause = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                m_bIsPaused = true;
                SUIManager.Instance.iPauseUI.gameObject.SetActive(true);
                AudioListener.pause = true;
                Time.timeScale = 0.0f;
            }
        }
    }
}
