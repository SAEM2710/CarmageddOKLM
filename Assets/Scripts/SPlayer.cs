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
	void Update ()
    {
        Shoot();
	}

    void OnCollisionEnter(Collision _Collision)
    {
        if(_Collision.gameObject.CompareTag("AI"))
        {
            if (m_ccCarController.CurrentSpeed > m_fMinSpeedToKill)
            {
                Destroy(_Collision.gameObject);
            }
        }
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
}
