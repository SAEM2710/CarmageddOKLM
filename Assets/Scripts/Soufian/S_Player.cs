using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// S_Player requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class S_Player : S_Character
{
    [SerializeField] private float m_fMinSpeedToKill;
    [SerializeField] private float m_fMaxBerzerkValue;
    [SerializeField] private GameObject[] m_goTabWeapons;
    [SerializeField] private GameObject[] m_goTabShields;
    [SerializeField] private GameObject m_goTurret;
    [SerializeField] private float m_fMaxShieldValue;
    [SerializeField] private GameObject m_goSpecialPower;
    [SerializeField] protected AudioClip specialPowerSound;
    [SerializeField] protected AudioClip specialPowerReady;
    [SerializeField] protected AudioClip careForLife;
    [SerializeField] protected AudioClip[] gameOverSounds;
    [SerializeField] protected AudioClip deathSound;
    [SerializeField] protected AudioClip[] voicesOnShieldLoss;

    private AudioSource m_asAudio;

    private float m_fCurrentBerzerkValue;
    private bool IsDead;
    private UnityStandardAssets.Vehicles.Car.CarController m_ccCarController;
    private float m_fCurrentShieldValue;
    private bool m_bShieldActivated;
    private ShieldType m_stShieldType;
    private bool isReadySoundPlayed = false;
    private bool isCareSoundPlayed = false;
    private float timeBeforeGO;
    private float timecpt = 0f;

    #region Getters/Setters

    public ShieldType stShieldType
    {
        get
        {
            return m_stShieldType;
        }
        set
        {
            m_stShieldType = value;
        }
    }

    public bool bShieldActivated
    {
        get
        {
            return m_bShieldActivated;
        }
        set
        {
            m_bShieldActivated = value;
        }
    }

    public float fCurrentShieldValue
    {
        get
        {
            return m_fCurrentShieldValue;
        }
        set
        {
            m_fCurrentShieldValue = value;
        }
    }

    public float fMaxShieldValue
    {
        get
        {
            return m_fMaxShieldValue;
        }
        set
        {
            m_fMaxShieldValue = value;
        }
    }

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

    #endregion

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        m_asAudio = GetComponent<AudioSource>();
        m_fCurrentBerzerkValue = 0f;
        m_ccCarController = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        IsDead = false;
        m_bShieldActivated = false;
    }

    public override void LoseLife(float _fDamage)
    {
        if (m_bShieldActivated)
        {
            m_fCurrentShieldValue -= _fDamage;
        }
        else
        {
            m_fCurrentLife -= _fDamage;
        }
        if (m_fCurrentLife < m_fMaxLife/4 && !isCareSoundPlayed)
        {
            m_asAudio.PlayOneShot(careForLife);
            isCareSoundPlayed = true;
        }
    }

    public override void Death()
    {
        base.Death();

        if (m_fCurrentLife <= 0f)
        {
            if (!IsDead)
            {
                int number = Random.Range(0, gameOverSounds.Length);
                m_asAudio.PlayOneShot(gameOverSounds[number], 5.0f);
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                GameObject goFXDestruction;
                goFXDestruction = Instantiate(m_goFXDestruction, transform.position, m_goFXDestruction.transform.rotation) as GameObject;
                SetScore();
                IsDead = true;
                timeBeforeGO = gameOverSounds[number].length + 2.0f;
                transform.Find("voiture").gameObject.SetActive(false);
            }
        }
    }

    private void goToGO()
    {
        timecpt += Time.deltaTime;
        if (timecpt >= timeBeforeGO)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("S_GameOver");
        }
    }

    private void SetScore()
    {
        PlayerPrefs.SetInt("KilledEnemies", S_GameManager.Instance.iKilledEnemies);
        PlayerPrefs.SetFloat("Time", Time.timeSinceLevelLoad);
    }

    private void DesactivateShield()
    {
        if (m_bShieldActivated)
        {
            if (m_fCurrentShieldValue <= 0)
            {
                m_bShieldActivated = false;
                DesactivateShieldAndWeapon();
                m_goTurret.GetComponent<TowerShoot>().ActiveShoot();
                int number = Random.Range(0, voicesOnShieldLoss.Length);
                m_asAudio.PlayOneShot(voicesOnShieldLoss[number], 5.0f);
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        ActivateBerzerk();
        DesactivateShield();
        if (IsDead)
        {
            Debug.Log("IsDead");
            goToGO();
        }
    }

    //MUST CLEAN IT
    private void SetShield(ShieldType _ShieldType)
    {
        switch (_ShieldType)
        {
            case ShieldType.Poison:
                ActivateShieldAndWeapon(0);
                m_goTurret.GetComponent<TowerShoot>().CancelShoot();
                break;
            case ShieldType.Fire:
                ActivateShieldAndWeapon(1);
                m_goTurret.GetComponent<TowerShoot>().CancelShoot();
                break;
            case ShieldType.Blade:
                ActivateShieldAndWeapon(2);
                m_goTurret.GetComponent<TowerShoot>().ActiveShoot();
                break;
        }
    }

    private void ActivateShieldAndWeapon(int _iIndice)
    {
        DesactivateShieldAndWeapon();
        m_goTabWeapons[_iIndice].SetActive(true);
        m_goTabShields[_iIndice].SetActive(true);
    }

    private void DesactivateShieldAndWeapon()
    {
        for (int i = 0; i < m_goTabShields.Length; ++i)
        {
            m_goTabWeapons[i].SetActive(false);
        }
        for (int i = 0; i < m_goTabShields.Length; ++i)
        {
            m_goTabShields[i].SetActive(false);
        }
    }

    private void ActivateBerzerk()
    {
        if(m_fCurrentBerzerkValue >= m_fMaxBerzerkValue)
        {
            if (!isReadySoundPlayed)
            {
                m_asAudio.PlayOneShot(specialPowerReady);
                isReadySoundPlayed = true;
            }

            m_fCurrentBerzerkValue = m_fMaxBerzerkValue;
            if(Input.GetButtonDown("Fire1"))
            {
                m_asAudio.PlayOneShot(specialPowerSound);
                isReadySoundPlayed = false;
                //Instantiate effect
                Instantiate(m_goSpecialPower, transform.position, transform.rotation);
                GameObject[] goTabAI = GameObject.FindGameObjectsWithTag("AI");
                for (int i = 0; i < goTabAI.Length; ++i)
                {
                    if(goTabAI[i].transform.GetChild(2).GetComponent<S_Visibility>().bIsVisible)
                    {
                        goTabAI[i].GetComponent<S_AI>().fCurrentLife = 0f;
                    }
                }
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
                LoseLife(5f);
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
            m_bShieldActivated = true;
            m_fCurrentShieldValue = m_fMaxShieldValue;
            m_stShieldType = _cCollider.gameObject.GetComponent<S_Shield>().btShield;
            SetShield(m_stShieldType);
            _cCollider.gameObject.GetComponent<S_Shield>().Death();
        }
    }

    #endregion
}
