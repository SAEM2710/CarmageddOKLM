using UnityEngine;
using System.Collections;

public class SSpawn : MonoBehaviour
{
    #region Visible Variables

    [SerializeField] private int m_iMaxCptIA;
    [SerializeField] private float m_fFrequence;
    [SerializeField] private GameObject[] m_goTabAI;

    #endregion

    private int m_iCurrentCptAI;
    private float m_fTime;

    // Use this for initialization
    void Start ()
    {
        m_fTime = 0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        SpawnIA();
    }

    void SpawnIA()
    {
        int RandomInt;
        RandomInt = Random.Range(0, m_goTabAI.Length);
        GameObject RandomAI;
        RandomAI = m_goTabAI[RandomInt];

        if (m_iCurrentCptAI < m_iMaxCptIA)
        {
            if (m_fTime > m_fFrequence)
            {
                m_fTime = 0f;

                GameObject goIA;
                goIA = Instantiate(RandomAI, transform.position, transform.rotation) as GameObject;
                ++m_iCurrentCptAI;
            }
            m_fTime += Time.deltaTime;
        }
    }
}
