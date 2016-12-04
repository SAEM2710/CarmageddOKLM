using UnityEngine;
using System.Collections;

// S_AI requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class S_AI : S_Character
{
    [SerializeField] private GameObject m_goBloodPuddle;
    [SerializeField] private GameObject[] m_goTabShieldBonus;
    [SerializeField] private float m_fMaxDistanceToShoot;
    [SerializeField] private AudioClip AIshootSound;
    [SerializeField] private AudioClip[] m_acTabSmash;

    private bool m_bIsTouchingObstacle;
    private int m_iChanceToDrop;
    private GameObject m_goShieldBonus;
    private GameObject m_goPlayer;
    private AudioSource m_asAudio;

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
    protected override void Start()
    {
        base.Start();

        m_goPlayer = GameObject.FindGameObjectWithTag("Player");
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = m_goPlayer.transform;
        m_bIsTouchingObstacle = false;
        m_iChanceToDrop = Random.Range(0, 10); //10%
        RandomShieldBonus();
        m_asAudio = GetComponent<AudioSource>();
    }

    protected override void Shoot(GameObject _goBullet)
    {
        base.Shoot(_goBullet);
        if (m_goPlayer)
        {
            float fDistance;
            fDistance = Vector3.Distance(m_goPlayer.transform.position, transform.position);
            if (fDistance <= m_fMaxDistanceToShoot)
            {
                if (m_fTime > m_fShootFrequence)
                {
                    m_fTime = 0f;

                    GameObject goProjectile;
                    goProjectile = Instantiate(_goBullet, m_v3PositionShoot, m_v3RotationShoot) as GameObject;

                    m_asAudio.PlayOneShot(AIshootSound);

                    Rigidbody rProjectileRb = goProjectile.GetComponent<Rigidbody>();
                    rProjectileRb.velocity = transform.TransformDirection(Vector3.forward * m_fShootForce) + m_rRigidbody.velocity;
                }
                m_fTime += Time.deltaTime;
            }
        }
    }

    public override void Death()
    {
        base.Death();

        if (m_fCurrentLife <= 0f)
        {
            GameObject goFXDestruction, goBloodPuddle;
            goFXDestruction = Instantiate(m_goFXDestruction, transform.position, m_goFXDestruction.transform.rotation) as GameObject;
            goBloodPuddle = Instantiate(m_goBloodPuddle, transform.position, transform.rotation) as GameObject;
            DropShield();
            ++S_GameManager.Instance.iKilledEnemies;
            --S_GameManager.Instance.iCurrentAICpt;

            int number = Random.Range(0, m_acTabSmash.Length);
            AudioSource.PlayClipAtPoint(m_acTabSmash[number], transform.position);

            Destroy(gameObject);
        }
    }

    private void RandomShieldBonus()
    {
        int RandomInt;
        RandomInt = Random.Range(0, m_goTabShieldBonus.Length);
        m_goShieldBonus = m_goTabShieldBonus[RandomInt];
    }

    private void /*IEnumerator*/ DropShield()
    {
        if(m_iChanceToDrop == 0)
        {
            GameObject goShieldBonus;
            //yield return new WaitForSeconds(1f);
            goShieldBonus = Instantiate(m_goShieldBonus, transform.position, transform.rotation) as GameObject;
        }
    }

    #region Collisions

    protected override void OnCollisionEnter(Collision _cCollision)
    {
        base.OnCollisionEnter(_cCollision);

        if (_cCollision.gameObject.CompareTag("Obstacle"))
        {
            m_bIsTouchingObstacle = true;
        }
    }

    protected override void OnCollisionStay(Collision _cCollision)
    {
        base.OnCollisionStay(_cCollision);

        if (_cCollision.gameObject.CompareTag("Obstacle"))
        {
            m_bIsTouchingObstacle = true;
        }
    }

    private void OnCollisionExit(Collision _cCollision)
    {
        if (_cCollision.gameObject.CompareTag("Obstacle"))
        {
            m_bIsTouchingObstacle = false;
        }
    }

    #endregion
}
