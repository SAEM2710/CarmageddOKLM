using UnityEngine;
using System.Collections;

public enum ShieldType
{
    Poison,
    Fire,
    Blade,
    Thorn
}

public class SShield : MonoBehaviour
{
    private ShieldType m_btShield;

    public ShieldType btShield
    {
        get
        {
            return m_btShield;
        }
        set
        {
            m_btShield = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        int IDShield;
        IDShield = Random.Range(0, 4);
        RandomShield(IDShield);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void RandomShield(int _ShieldID)
    {
        switch (_ShieldID)
        {
            case 0:
                m_btShield = ShieldType.Poison;
                break;
            case 1:
                m_btShield = ShieldType.Fire;
                break;
            case 2:
                m_btShield = ShieldType.Blade;
                break;
            case 3:
                m_btShield = ShieldType.Thorn;
                break;
        }
    }
}
