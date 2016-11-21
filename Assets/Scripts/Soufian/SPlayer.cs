using UnityEngine;
using System.Collections;

public class SPlayer : SEntity
{
    #region Visible Variables

    [SerializeField] private float m_fMinSpeedToKill;

    #endregion

    private UnityStandardAssets.Vehicles.Car.CarController m_ccCarController;
    private Rigidbody m_rRigidbody;

    // Use this for initialization
    void Start ()
    {
        m_ccCarController = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        m_rRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Debug.Log("Speed : " + m_ccCarController.CurrentSpeed);
        Shoot();
	}

    protected override void Shoot()
    {
        base.Shoot();

        if (Input.GetButton("Fire1"))
        {
            if (m_fTime > m_fShootFrequence)
            {
                m_fTime = 0f;

                GameObject goProjectile;
                goProjectile = Instantiate(m_goProjectile, m_v3PositionShoot, m_v3RotationShoot) as GameObject;
                goProjectile.GetComponent<SProjectile>().bIsSpawnByPlayer = true;

                //Play Sound

                Rigidbody rProjectileRb = goProjectile.GetComponent<Rigidbody>();
                Debug.Log(m_rRigidbody);
                rProjectileRb.velocity = transform.TransformDirection(Vector3.forward * m_fShootForce) + m_rRigidbody.velocity;
            }
            m_fTime += Time.deltaTime;
        }
    }

    private void SetShield(ShieldType _ShieldType)
    {
        switch (_ShieldType)
        {
            case ShieldType.Poison:
                Debug.Log("Poison activated");
                break;
            case ShieldType.Fire:
                Debug.Log("Fire activated");
                break;
            case ShieldType.Blade:
                Debug.Log("Blade activated");
                break;
            case ShieldType.Thorn:
                Debug.Log("Thorn activated");
                break;
        }
    }

    #region Collisions

    void OnCollisionEnter(Collision _cCollision)
    {
        if (_cCollision.gameObject.CompareTag("AI"))
        {
            if (m_ccCarController.CurrentSpeed > m_fMinSpeedToKill)
            {
                _cCollision.gameObject.GetComponent<SAI>().LoseLife(10f);
            }
            else if(_cCollision.gameObject.GetComponent<SAI>().bIsTouchingObstacle)
            {
                _cCollision.gameObject.GetComponent<SAI>().LoseLife(10f);
            }
        }
    }

    void OnCollisionStay(Collision _cCollision)
    {
        if (_cCollision.gameObject.CompareTag("AI"))
        {
            if (m_ccCarController.CurrentSpeed > m_fMinSpeedToKill)
            {
                _cCollision.gameObject.GetComponent<SAI>().LoseLife(10f);
            }
            else if (_cCollision.gameObject.GetComponent<SAI>().bIsTouchingObstacle)
            {
                _cCollision.gameObject.GetComponent<SAI>().LoseLife(10f);
            }
        }
    }

    #endregion

    #region Triggers

    void OnTriggerEnter(Collider _cCollider)
    {
        if (_cCollider.CompareTag("ShieldBox"))
        {
            ShieldType shieldtype;
            shieldtype = _cCollider.transform.GetChild(0).GetComponent<SShield>().btShield;
            SetShield(shieldtype);
            //Do something
            Destroy(_cCollider.gameObject);
        }
    }

    #endregion
}
