using UnityEngine;
using System.Collections;

enum BonusType
{
    Poison,
    Fire,
    Blade,
    Thorn
}

public class Bonus : MonoBehaviour
{
    public BonusType m_btBonus;

    /*public BonusType btBonus
    {
        get
        {
            return m_btBonus;
        }
        set
        {
            m_btBonus = value;
        }
    }*/

    // Use this for initialization
    void Start ()
    {
        int RandomBonus;
        RandomBonus = Random.Range(0, 4);
        switch(RandomBonus)
        {
            case 0:
                m_btBonus = BonusType.Poison;
                break;
            case 1:
                m_btBonus = BonusType.Fire;
                break;
            case 2:
                m_btBonus = BonusType.Blade;
                break;
            case 3:
                m_btBonus = BonusType.Thorn;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
