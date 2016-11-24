using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SPlayerScore : MonoBehaviour
{
    //Classe Test
    [SerializeField] private Text m_tScoreText;
    [SerializeField] private Text m_tTimeText;

    // Use this for initialization
    void Start ()
    {
        m_tTimeText.text = "Time : " + PlayerPrefs.GetFloat("Time").ToString();
        m_tScoreText.text = "Score : " + PlayerPrefs.GetInt("KilledEnemies").ToString();
    }
}
