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

    /*private float m_fBestTime;
    private int m_iBestScore;*/
    // Use this for initialization
    void Start ()
    {
        /*m_fBestTime = PlayerPrefs.GetFloat("BestTime");
        m_iBestScore = PlayerPrefs.GetInt("BestScore");*/

        float fTime = PlayerPrefs.GetFloat("Time");
        string fMinutes = Mathf.Floor(fTime / 60).ToString("00");
        string fSeconds = (fTime % 60).ToString("00");

        int iScore = PlayerPrefs.GetInt("KilledEnemies");

        m_tTimeText.text = fMinutes + ":" + fSeconds + "of your life were wasted forever";
        m_tScoreText.text = iScore.ToString() + " innocent people died during this game";

        /*if(PlayerPrefs.GetFloat("BestTime") == null)
        {
            PlayerPrefs.SetFloat("BestTime", 0f);
        }
        if (PlayerPrefs.GetInt("BestScore") == null)
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }*/

        /*if (fTime > m_fBestTime)
        {
            PlayerPrefs.SetFloat("BestTime", fTime);
        }

        if (iScore > m_iBestScore)
        {
            PlayerPrefs.SetInt("BestScore", iScore);
        }

        m_tBestTimeText.text = "Best Time : " + m_fBestTime.ToString();
        m_tBestScoreText.text = "Best Score : " + m_iBestScore.ToString();*/
    }
}
