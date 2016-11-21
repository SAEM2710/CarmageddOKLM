using UnityEngine;
using System.Collections;

public class SAI : SEntity
{
    #region Visible Variables

    [SerializeField] private GameObject m_goBloodPuddle;
    
    #endregion

    private Rigidbody m_rRigidbody;
    private bool m_bIsTouchingObstacle;

    #region Getters/Setters

    public bool bIsTouchingObstacle
    {
        get
        {
            return m_bIsTouchingObstacle;
        }
        set
        {
            m_bIsTouchingObstacle = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start ()
    {
        GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = goPlayer.transform;
        m_rRigidbody = GetComponent<Rigidbody>();
        m_bIsTouchingObstacle = false;

        //m_fShootFrequence = 3f; //A VOIR
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Shoot();
	}

    protected override void Shoot()
    {
        base.Shoot();

        if (m_fTime > m_fShootFrequence)
        {
            m_fTime = 0f;

            GameObject goProjectile;
            goProjectile = Instantiate(m_goProjectile, m_v3PositionShoot, m_v3RotationShoot) as GameObject;

            //Play Sound

            Rigidbody rProjectileRb = goProjectile.GetComponent<Rigidbody>();
            rProjectileRb.velocity = transform.TransformDirection(Vector3.forward * m_fShootForce) + m_rRigidbody.velocity;
        }
        m_fTime += Time.deltaTime;
    }

    public override void Death()
    {
        base.Death();

        if (m_fLife <= 0f)
        {
            GameObject goFXDestruction, goBloodPuddle;
            goFXDestruction = Instantiate(m_goFXDestruction, transform.position, transform.rotation) as GameObject;
            goBloodPuddle = Instantiate(m_goBloodPuddle, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }

    private void DropShield()
    {

    }

    #region Collisions

    void OnCollisionEnter(Collision _cCollision)
    {
        if (_cCollision.gameObject.CompareTag("Obstacle"))
        {
            m_bIsTouchingObstacle = true;
        }
    }

    void OnCollisionStay(Collision _cCollision)
    {
        if (_cCollision.gameObject.CompareTag("Obstacle"))
        {
            m_bIsTouchingObstacle = true;
        }
    }

    void OnCollisionExit(Collision _cCollision)
    {
        if (_cCollision.gameObject.CompareTag("Obstacle"))
        {
            m_bIsTouchingObstacle = false;
        }
    }

    #endregion
}
