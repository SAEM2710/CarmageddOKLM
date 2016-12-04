using UnityEngine;
using System.Collections;

public class S_AITowerShoot : S_Character
{
    [SerializeField] private float m_fMaxDistanceToShoot;
    [SerializeField] private AudioClip AIshootSound;
    [SerializeField] private GameObject[] m_goShootOrigin;

    private GameObject m_goPlayer;
    private AudioSource m_asAudio;


    // Use this for initialization
    protected override void Start ()
    {
        //base.Start();
        m_fTime = 0f;
        m_v3PositionShoot = transform.GetChild(0).position;
        m_v3RotationShoot = transform.GetChild(0).rotation;
        m_rRigidbody = GetComponentInParent<Rigidbody>();
        //m_fCurrentLife = m_fMaxLife;

        m_goPlayer = GameObject.FindGameObjectWithTag("Player");
        m_asAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        transform.LookAt(m_goPlayer.transform);
        base.FixedUpdate();     
    }

    protected override void Shoot(GameObject _goBullet)
    {
        //base.Shoot(_goBullet);

        float fDistance;
        fDistance = Vector3.Distance(m_goPlayer.transform.position, transform.position);
        if (fDistance <= m_fMaxDistanceToShoot)
        {
            if (m_fTime > m_fShootFrequence)
            {
                m_fTime = 0f;

                GameObject goProjectile;
                for(int i = 0; i < m_goShootOrigin.Length; ++i)
                {
                    goProjectile = Instantiate(_goBullet, m_goShootOrigin[i].transform.position, m_goShootOrigin[i].transform.rotation) as GameObject;
                    Rigidbody rProjectileRb = goProjectile.GetComponent<Rigidbody>();
                    rProjectileRb.velocity = transform.TransformDirection(Vector3.forward * m_fShootForce) + m_rRigidbody.velocity;
                }

                m_asAudio.PlayOneShot(AIshootSound);
            }
            m_fTime += Time.deltaTime;
        }
    }
}
