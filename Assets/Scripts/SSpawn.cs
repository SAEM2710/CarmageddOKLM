using UnityEngine;
using System.Collections;

public class SSpawn : MonoBehaviour
{
    #region Visible Variables

    [SerializeField] private int m_iMaxCptIA;
    [SerializeField] private float m_fFrequence;
    [SerializeField] private GameObject m_goIA;

    #endregion

    private int m_iCurrentCptIA;
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
        if (m_iCurrentCptIA < m_iMaxCptIA)
        {
            if (m_fTime > m_fFrequence)
            {
                m_fTime = 0f;

                GameObject goIA;
                goIA = Instantiate(m_goIA, transform.position, transform.rotation) as GameObject;
                ++m_iCurrentCptIA;
            }
            m_fTime += Time.deltaTime;
        }
    }
}
