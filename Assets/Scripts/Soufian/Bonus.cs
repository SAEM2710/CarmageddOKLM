using UnityEngine;
using System.Collections;

public enum BonusType
{
    Poison,
    Fire,
    Blade,
    Thorn
}

public class Bonus : MonoBehaviour
{
    private BonusType m_btBonus;

    public BonusType btBonus
    {
        get
        {
            return m_btBonus;
        }
        set
        {
            m_btBonus = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        int IDBonus;
        IDBonus = Random.Range(0, 4);
        RandomBonus(IDBonus);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void RandomBonus(int _BonusID)
    {
        switch (_BonusID)
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
}
