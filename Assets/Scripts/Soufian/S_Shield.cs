using UnityEngine;
using System.Collections;

public enum ShieldType
{
    Poison,
    Fire,
    Blade,
    Thorn
}

public class S_Shield : S_Object
{
    [SerializeField] private GameObject m_goFeedBack;
    [SerializeField] private float m_fRotationSpeed;

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
    protected override void Start ()
    {
        base.Start();

        string ShieldTag;
        ShieldTag = transform.GetChild(0).tag;
        SetShieldType(ShieldTag);
	}

    protected override void Update()
    {
        //base.Update();

        transform.Rotate(Vector3.up * m_fRotationSpeed * Time.deltaTime);
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

    public override void Death()
    {
        base.Death();

        GameObject goFXDestruction, goFeedBack;
        goFXDestruction = Instantiate(m_goFXDestruction, transform.position, transform.rotation) as GameObject;
        goFeedBack = Instantiate(m_goFeedBack, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
    }
}
