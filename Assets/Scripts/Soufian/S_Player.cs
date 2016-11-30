using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// S_Player requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class S_Player : S_Character
{
    [SerializeField] private float m_fMinSpeedToKill;
    [SerializeField] private float m_fMaxBerzerkValue;

    private float m_fCurrentBerzerkValue;
    private bool IsDead;
    private UnityStandardAssets.Vehicles.Car.CarController m_ccCarController;

    public float fCurrentBerzerkValue
    {
        get
        {
            return m_fCurrentBerzerkValue;
        }
        set
        {
            m_fCurrentBerzerkValue = value;
        }
    }

    public float fMaxBerzerkValue
    {
        get
        {
            return m_fMaxBerzerkValue;
        }
        set
        {
            m_fMaxBerzerkValue = value;
        }
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        m_fCurrentBerzerkValue = 0f;
        m_ccCarController = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        IsDead = false;
    }

    /*protected override void Shoot(GameObject _goBullet)
    {
        base.Shoot(_goBullet);

        if (Input.GetButton("Fire1"))
        {
            if (m_fTime > m_fShootFrequence)
            {
                m_fTime = 0f;

                GameObject goProjectile;
                goProjectile = Instantiate(_goBullet, m_v3PositionShoot, m_v3RotationShoot) as GameObject;
                goProjectile.GetComponent<S_Bullet>().bIsSpawnByPlayer = true;

                //Play Sound

                Rigidbody rProjectileRb = goProjectile.GetComponent<Rigidbody>();
                rProjectileRb.velocity = transform.TransformDirection(Vector3.forward * m_fShootForce) + m_rRigidbody.velocity;
            }
            m_fTime += Time.deltaTime;
        }
    }*/

    public override void Death()
    {
        base.Death();

        if (m_fCurrentLife <= 0f)
        {
            if (!IsDead)
            {
                //Play Sound
                GameObject goFXDestruction;
                goFXDestruction = Instantiate(m_goFXDestruction, transform.position, m_goFXDestruction.transform.rotation) as GameObject;
                PlayerPrefs.SetInt("KilledEnemies", S_GameManager.Instance.iKilledEnemies);
                PlayerPrefs.SetFloat("Time", Time.timeSinceLevelLoad);
                IsDead = true;
                Destroy(gameObject);
                SceneManager.LoadScene("GameOver");

            }
        }
    }

    protected override void Update()
    {
        base.Update();

        ActivateBerzerk();
        Debug.Log("CurrentBerzerkValue : " + m_fCurrentBerzerkValue);
    }

    //MUST CLEAN IT
    private void SetShield(ShieldType _ShieldType)
    {
        switch (_ShieldType)
        {
            case ShieldType.Poison:
                transform.GetChild(5).GetChild(0).GetChild(2).gameObject.SetActive(true);

                transform.GetChild(5).GetChild(0).GetChild(1).gameObject.SetActive(false);
                transform.GetChild(5).GetChild(0).GetChild(3).gameObject.SetActive(false);
                Debug.Log("Poison activated");
                break;
            case ShieldType.Fire:
                transform.GetChild(5).GetChild(0).GetChild(1).gameObject.SetActive(true);

                transform.GetChild(5).GetChild(0).GetChild(3).gameObject.SetActive(false);
                transform.GetChild(5).GetChild(0).GetChild(2).gameObject.SetActive(false);
                Debug.Log("Fire activated");
                break;
            case ShieldType.Blade:
                transform.GetChild(5).GetChild(0).GetChild(3).gameObject.SetActive(true);

                transform.GetChild(5).GetChild(0).GetChild(2).gameObject.SetActive(false);
                transform.GetChild(5).GetChild(0).GetChild(1).gameObject.SetActive(false);
                Debug.Log("Blade activated");
                break;
            case ShieldType.Thorn:
                Debug.Log("Thorn activated");
                break;
        }
    }

    private void ActivateBerzerk()
    {
        if(m_fCurrentBerzerkValue >= m_fMaxBerzerkValue)
        {
            m_fCurrentBerzerkValue = m_fMaxBerzerkValue;
            if(Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Berzerk");
                //Play Sound
                //Instantiate effect
                //Do something
                m_fCurrentBerzerkValue = 0f;
            }
        }
    }

    #region Collisions

    protected override void OnCollisionEnter(Collision _cCollision)
    {
        base.OnCollisionEnter(_cCollision);

        if (_cCollision.gameObject.CompareTag("AI"))
        {
            if (m_ccCarController.CurrentSpeed > m_fMinSpeedToKill)
            {
                _cCollision.gameObject.GetComponent<S_AI>().LoseLife(10f);
                m_fCurrentBerzerkValue += 5f;
            }
            else if(_cCollision.gameObject.GetComponent<S_AI>().bIsTouchingObstacle)
            {
                _cCollision.gameObject.GetComponent<S_AI>().LoseLife(10f);
                //m_fCurrentBerzerkValue -= 2.5f;
            }
        }
    }

    protected override void OnCollisionStay(Collision _cCollision)
    {
        base.OnCollisionStay(_cCollision);

        if (_cCollision.gameObject.CompareTag("AI"))
        {
            if (m_ccCarController.CurrentSpeed > m_fMinSpeedToKill)
            {
                _cCollision.gameObject.GetComponent<S_AI>().LoseLife(10f);
                //m_fCurrentBerzerkValue += 5f;
            }
            else if (_cCollision.gameObject.GetComponent<S_AI>().bIsTouchingObstacle)
            {
                _cCollision.gameObject.GetComponent<S_AI>().LoseLife(10f);
                //m_fCurrentBerzerkValue += 5f;
            }
        }
    }

    #endregion

    #region Triggers

    private void OnTriggerEnter(Collider _cCollider)
    {
        if (_cCollider.CompareTag("ShieldBox"))
        {
                ShieldType shieldtype;
                shieldtype = _cCollider.gameObject.GetComponent<S_Shield>().btShield;
                SetShield(shieldtype);
                _cCollider.gameObject.GetComponent<S_Shield>().Death();
        }
    }

    #endregion
}
