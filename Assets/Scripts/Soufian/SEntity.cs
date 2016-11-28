using UnityEngine;
using System.Collections;

public class SEntity : MonoBehaviour 
{
    #region Visible Variables

    [SerializeField] protected GameObject m_goProjectile;
    [SerializeField] protected float m_fShootFrequence;
    [SerializeField] protected float m_fShootForce;

    [SerializeField] protected float m_fMaxLife;
    [SerializeField] protected GameObject m_goFXDestruction;

    #endregion

    protected float m_fTime;
    protected Vector3 m_v3PositionShoot;
    protected Quaternion m_v3RotationShoot;
    protected float m_fCurrentLife;

    #region Getters/Setters

    public float fCurrentLife
    {
        get
        {
            return m_fCurrentLife;
        }
        set
        {
            m_fCurrentLife = value;
        }
    }

    public float fMaxLife
    {
        get
        {
            return m_fMaxLife;
        }
        set
        {
            m_fMaxLife = value;
        }
    }

    #endregion

    // Use this for initialization
    protected virtual void Start () 
	{
        m_fTime = 0f;
        m_v3PositionShoot = transform.GetChild(0).position;
        m_v3RotationShoot = transform.GetChild(0).rotation;
        m_fCurrentLife = m_fMaxLife;
    }
	
	// Update is called once per frame
	void Update () 
	{
        Death();
    }

    protected virtual void FixedUpdate()
    {
        Shoot();
    }

    protected virtual void Shoot()
	{
        m_v3PositionShoot = transform.GetChild(0).position;
        m_v3RotationShoot = transform.GetChild(0).rotation;
    }

    public virtual void LoseLife(float _fDamage)
    {
        m_fCurrentLife -= _fDamage;
    }

    public virtual void Death()
    {

    }
}
