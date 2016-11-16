using UnityEngine;
using System.Collections;

public class SEntity : MonoBehaviour 
{
    #region Visible Variables

    [SerializeField] protected GameObject m_goProjectile;
    [SerializeField] protected float m_fShootFrequence;
    [SerializeField] protected float m_fShootForce;
    [SerializeField] protected float m_fLife;

    #endregion

    protected float m_fTime;
    protected Vector3 m_v3PositionShoot;
    protected Quaternion m_v3RotationShoot;

    // Use this for initialization
    void Start () 
	{
        m_fTime = 0f;
        m_v3PositionShoot = transform.GetChild(0).position;
        m_v3RotationShoot = transform.GetChild(0).rotation;
    }
	
	// Update is called once per frame
	void Update () 
	{
        Death();
    }

	protected virtual void Shoot()
	{
        m_v3PositionShoot = transform.GetChild(0).position;
        m_v3RotationShoot = transform.GetChild(0).rotation;
    }

    public virtual void LoseLife(float _fDamage)
    {
        m_fLife -= _fDamage;
    }

    protected virtual void Death()
    {
        if(m_fLife <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
