using UnityEngine;
using System.Collections;

public class S_Visibility : MonoBehaviour
{
    private bool m_bIsVisible;

    #region Getters/Setters

    public bool bIsVisible
    {
        get
        {
            return m_bIsVisible;
        }
        set
        {
            m_bIsVisible = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start ()
    {
        m_bIsVisible = false;
    }
	
    private void OnBecameVisible()
    {
        m_bIsVisible = true;
    }

    private void OnBecameInvisible()
    {
        m_bIsVisible = false;
    }
}
