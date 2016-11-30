using UnityEngine;
using System.Collections;

public class PoisonFireDamages : MonoBehaviour {

    protected bool isPsn = false;
    protected bool isFire = false;

    [SerializeField] protected float m_poisonTime = 5.0f;
    [SerializeField] protected float m_poisonFrequency = 0.5f;
    [SerializeField] protected float m_poisonDmgs = 1.0f;
    [SerializeField] protected float m_fireTime = 2.0f;
    [SerializeField] protected float m_fireFrequency = 0.5f;
    [SerializeField] protected float m_fireDmgs = 0.5f;

    private float time = 0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (isPsn)
        {
            if (time < m_poisonTime)
            {
                time += Time.deltaTime;
                if (time % m_poisonFrequency == 0) this.SendMessage("LoseLife", m_poisonDmgs);
            }
            else
            {
                time = 0;
                isPsn = false;
            }
        }

        if (isFire)
        {
            if (time < m_fireTime)
            {
                time += Time.deltaTime;
                if (time % m_fireFrequency == 0) this.SendMessage("LoseLife", m_fireDmgs);
            }
            else
            {
                time = 0;
                isFire = false;
            }
        }
    }

    void getPoisoned ()
    {
        isPsn = true;
    }

    void getBurned()
    {
        isFire = true;
    }
}
