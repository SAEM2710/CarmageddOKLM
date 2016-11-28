using UnityEngine;
using System.Collections;

public class S_Bullet : S_Object
{
    private bool m_bIsSpawnByPlayer;
    private float m_fDamage;

    #region Getters/Setters

    public bool bIsSpawnByPlayer
    {
        get
        {
            return m_bIsSpawnByPlayer;
        }
        set
        {
            m_bIsSpawnByPlayer = value;
        }
    }

    public float fDamage
    {
        get
        {
            return m_fDamage;
        }
        set
        {
            m_fDamage = value;
        }
    }

    #endregion
}
