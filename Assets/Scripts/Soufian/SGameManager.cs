using UnityEngine;
using System.Collections;

public class SGameManager : GenericSingleton<SGameManager>
{
    private int m_iKilledEnemies;

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
        m_iKilledEnemies = 0;
    }
}
