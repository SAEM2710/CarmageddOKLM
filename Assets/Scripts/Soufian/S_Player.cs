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

    private float m_fCurrentBerzerkValue;
    private bool IsDead;
    private UnityStandardAssets.Vehicles.Car.CarController m_ccCarController;
    private float m_fMaxShieldValue;
    private float m_fCurrentShieldValue;
    private bool m_bShieldActivated;

    #region Getters/Setters

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
    }

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
                SetScore();
                IsDead = true;
                Destroy(gameObject);
                SceneManager.LoadScene("GameOver");

            }
        }
    }

    private void SetScore()
    {
        PlayerPrefs.SetInt("KilledEnemies", S_GameManager.Instance.iKilledEnemies);
        PlayerPrefs.SetFloat("Time", Time.timeSinceLevelLoad);
    }

    private void DesactivateShield()
    {
        if(m_fCurrentShieldValue <= 0)
        {
            m_bShieldActivated = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        ActivateBerzerk();
        /*Debug.Log("CurrentShieldValue : " + m_fCurrentShieldValue);
        Debug.Log("MaxShieldValue : " + m_fMaxShieldValue);
        Debug.Log(m_bShieldActivated);*/
        DesactivateShield();
    }

    //MUST CLEAN IT
    private void SetShield(ShieldType _ShieldType)
    {
        switch (_ShieldType)
        {
            case ShieldType.Poison:
                m_goTabWeapons[0].SetActive(true);
                m_goTabWeapons[1].SetActive(false);
                m_goTabWeapons[2].SetActive(false);
                Debug.Log("Poison activated");
                break;
            case ShieldType.Fire:
                m_goTabWeapons[0].SetActive(false);
                m_goTabWeapons[1].SetActive(true);
                m_goTabWeapons[2].SetActive(false);
                Debug.Log("Fire activated");
                break;
            case ShieldType.Blade:
                m_goTabWeapons[0].SetActive(false);
                m_goTabWeapons[1].SetActive(false);
                m_goTabWeapons[2].SetActive(true);
                Debug.Log("Blade activated");
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
            m_bShieldActivated = true;
            m_fMaxShieldValue = 100f;
            m_fCurrentShieldValue = m_fMaxShieldValue;
            ShieldType shieldtype;
            shieldtype = _cCollider.gameObject.GetComponent<S_Shield>().btShield;
            SetShield(shieldtype);
            _cCollider.gameObject.GetComponent<S_Shield>().Death();
        }
    }

    #endregion
}
