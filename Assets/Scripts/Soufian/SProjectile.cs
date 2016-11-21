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
                _cCollider.GetComponentInParent<SAI>().LoseLife(10f); //TO CHANGE
                //_cCollider.gameObject.GetComponent<SAI>().Death();
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
                _cCollider.GetComponentInParent<SPlayer>().LoseLife(10f); //TO CHANGE
                Destroy(gameObject);
            }
            else
            {
                //Destroy projectile allie ?
            }
        }
    }
}
