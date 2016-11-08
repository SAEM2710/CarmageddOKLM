using UnityEngine;
using System.Collections;

public class SAI : SEntity
{
    private Rigidbody m_rRigidbody;

    // Use this for initialization
    void Start ()
    {
        GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = goPlayer.transform;
        m_rRigidbody = GetComponent<Rigidbody>();

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
}
