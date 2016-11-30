using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class S_PlayerScore : MonoBehaviour
{
    //Classe Test
    [SerializeField] private Text m_tScoreText;
    [SerializeField] private Text m_tTimeText;
    [SerializeField] private Text m_tBestScoreText;
    [SerializeField] private Text m_tBestTimeText;

    // Use this for initialization
    void Start ()
    {
        float fTime = PlayerPrefs.GetFloat("Time");
        int iScore = PlayerPrefs.GetInt("KilledEnemies");

        m_tTimeText.text = "Time : " + fTime.ToString();
        m_tScoreText.text = "Score : " + iScore.ToString();

        if(PlayerPrefs.GetFloat("BestTime") == null)
        {
            PlayerPrefs.SetFloat("BestTime", 0f);
        }
        if (PlayerPrefs.GetInt("BestScore") == null)
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

        if (fTime > PlayerPrefs.GetFloat("BestTime"))
        {
            PlayerPrefs.SetFloat("BestTime", fTime);
        }

        if (iScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", iScore);
        }

        m_tBestTimeText.text = "Best Time : " + fTime.ToString();
        m_tBestScoreText.text = "Best Score : " + iScore.ToString();
    }
}
