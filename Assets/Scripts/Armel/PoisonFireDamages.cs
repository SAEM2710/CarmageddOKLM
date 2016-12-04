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

    [SerializeField] protected ParticleSystem m_poisonParticles;
    [SerializeField] protected ParticleSystem m_burningParticles;

    private float time;
    private int nbrTick;

    // Use this for initialization
    void Start () {
        time = 0f;
        nbrTick = 1;
        m_poisonParticles.Pause();
        m_burningParticles.Pause();
	}
	
	// Update is called once per frame
	void Update () {
	
        if (isPsn)
        {
            Debug.Log("PSN");
            m_poisonParticles.Play();
            if (time < m_poisonTime)
            {
                time += Time.deltaTime;
                if (time < m_poisonFrequency * nbrTick)
                {
                    this.SendMessage("LoseLife", m_poisonDmgs);
                    ++nbrTick;
                }
            }
            else
            {
                m_poisonParticles.Pause();
                time = 0;
                isPsn = false;
                nbrTick = 1;
            }
        }

        if (isFire)
        {
            Debug.Log("FIRE");
            m_burningParticles.Play();
            if (time < m_fireTime)
            {
                time += Time.deltaTime;
                if (time < m_fireFrequency * nbrTick)
                {
                    this.SendMessage("LoseLife", m_fireDmgs);
                    ++nbrTick;
                }
            }
            else
            {
                m_burningParticles.Pause();
                time = 0;
                isFire = false;
                nbrTick = 1;
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
