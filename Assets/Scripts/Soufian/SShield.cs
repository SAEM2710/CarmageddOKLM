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
    [SerializeField] private GameObject m_goFXDestruction1;
    [SerializeField] private GameObject m_goFXDestruction2;

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

    public void Death()
    {
        GameObject goFXDestruction1, goFXDestruction2;
        goFXDestruction1 = Instantiate(m_goFXDestruction1, transform.position, transform.rotation) as GameObject;
        goFXDestruction2 = Instantiate(m_goFXDestruction2, transform.position, transform.rotation) as GameObject;
        Destroy(transform.parent.gameObject);
    }
}
