using UnityEngine;
using System.Collections;

public class S_Character : S_Entity
{
    [SerializeField] protected GameObject m_goBullet;
    [SerializeField] protected float m_fShootFrequence;
    [SerializeField] protected float m_fShootForce;
    [SerializeField] protected float m_fMaxLife;

    protected float m_fCurrentLife;
    protected Vector3 m_v3PositionShoot;
    protected Quaternion m_v3RotationShoot;
    protected float m_fTime;
    protected Rigidbody m_rRigidbody;

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
    protected override void Start ()
    {
        base.Start();

        m_rRigidbody = GetComponent<Rigidbody>();
        m_fTime = 0f;
        m_v3PositionShoot = transform.GetChild(0).position;
        m_v3RotationShoot = transform.GetChild(0).rotation;
        m_fCurrentLife = m_fMaxLife;
    }
	
    protected void FixedUpdate()
    {
        Shoot(m_goBullet);
    }

    protected virtual void Shoot(GameObject _goBullet)
    {
        m_v3PositionShoot = transform.GetChild(0).position;
        m_v3RotationShoot = transform.GetChild(0).rotation;
    }

    public virtual void LoseLife(float _fDamage)
    {
        m_fCurrentLife -= _fDamage;
    }

    #region Collisions

    protected virtual void OnCollisionEnter(Collision _cCollision)
    {
        if (_cCollision.gameObject.CompareTag("Bullet"))
        {
            S_Bullet bBullet = _cCollision.gameObject.GetComponent<S_Bullet>();
            if (CompareTag("Player"))
            {
                if (!bBullet.bIsSpawnByPlayer)
                {
                    GetComponent<S_Player>().LoseLife(10f);
                    Destroy(_cCollision.gameObject);
                }
            }
            else if (CompareTag("AI"))
            {
                if (bBullet.bIsSpawnByPlayer)
                {
                    GetComponent<S_AI>().LoseLife(10f);
                    Destroy(_cCollision.gameObject);
                }
            }
        }
    }

    protected virtual void OnCollisionStay(Collision _cCollision)
    {

    }

    #endregion
}
