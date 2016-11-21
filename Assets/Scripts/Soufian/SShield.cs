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
        string ShieldTag;
        ShieldTag = tag;
        SetShieldType(ShieldTag);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void SetShieldType(string _ShieldTag)
    {
        switch (_ShieldTag)
        {
            case "PoisonBox":
                m_btShield = ShieldType.Poison;
                break;
            case "FireBox":
                m_btShield = ShieldType.Fire;
                break;
            case "BladeBox":
                m_btShield = ShieldType.Blade;
                break;
            case "ThornBox":
                m_btShield = ShieldType.Thorn;
                break;
        }
    }
}
