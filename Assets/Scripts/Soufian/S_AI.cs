using UnityEngine;
using System.Collections;

// S_AI requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class S_AI : S_Character
{
    [SerializeField] private GameObject m_goBloodPuddle;
    [SerializeField] private GameObject[] m_goTabShieldBonus;

    private bool m_bIsTouchingObstacle;
    private int m_iChanceToDrop;
    private GameObject m_goShieldBonus;
    private bool m_bIsVisible;

    #region Getters/Setters

    public bool bIsVisible
    {
        get
        {
            return m_bIsVisible;
        }
        set
        {
            m_bIsVisible = value;
        }
    }

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

        GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = goPlayer.transform;
        m_bIsTouchingObstacle = false;
        m_iChanceToDrop = Random.Range(0, 10); //10%
        RandomShieldBonus();
    }

    protected override void Shoot(GameObject _goBullet)
    {
        base.Shoot(_goBullet);

        if (m_fTime > m_fShootFrequence)
        {
            m_fTime = 0f;

            GameObject goProjectile;
            goProjectile = Instantiate(_goBullet, m_v3PositionShoot, m_v3RotationShoot) as GameObject;

            //Play Sound

            Rigidbody rProjectileRb = goProjectile.GetComponent<Rigidbody>();
            rProjectileRb.velocity = transform.TransformDirection(Vector3.forward * m_fShootForce) + m_rRigidbody.velocity;
        }
        m_fTime += Time.deltaTime;
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
            Destroy(gameObject);
            //StartCoroutine("DropShield");
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

    private void OnBecameVisible()
    {
        m_bIsVisible = true;
    }

    private void OnBecameInvisible()
    {
        m_bIsVisible = false;
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
