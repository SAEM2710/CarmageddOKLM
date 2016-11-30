using UnityEngine;
using System.Collections;

public class PoisonThrowerBehaviour : MonoBehaviour {


    [SerializeField] protected float m_damagesOnHit = 0.1f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("AI"))
        {
            other.SendMessage("LoseLife", m_damagesOnHit);
            other.SendMessage("getPoisoned");
        }
    }
}
