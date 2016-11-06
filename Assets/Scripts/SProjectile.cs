using UnityEngine;
using System.Collections;

public class SProjectile : MonoBehaviour
{
    private bool m_bIsSpawnByPlayer;

    #region Getters/Setters

    public bool bIsSpawnByPlayer
    {
        get
        {
            return m_bIsSpawnByPlayer;
        }
        set
        {
            m_bIsSpawnByPlayer = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start ()
    {
        Destroy(gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider _cCollider)
    {
        if (_cCollider.CompareTag("AI"))
        {
            if (m_bIsSpawnByPlayer)
            {
                Destroy(_cCollider.gameObject);
                Destroy(gameObject);
            }
            else
            {
                //Destroy projectile allie ?
            }
        }
        else if(_cCollider.CompareTag("PlayerCollider"))
        {
            if (!m_bIsSpawnByPlayer)
            {
                Debug.Log("--m_fLife");
                Destroy(gameObject);
            }
            else
            {
                //Destroy projectile allie ?
            }
        }
    }
}
