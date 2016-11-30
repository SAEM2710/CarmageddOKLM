using UnityEngine;
using System.Collections;

public class FireThrowerBehaviour : MonoBehaviour
{


    [SerializeField] protected float m_damagesOnHit = 5.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI"))
        {
            // GameObject otherGo = other.GetComponent<GameObject>();
            other.SendMessage("LoseLife", m_damagesOnHit);
            other.SendMessage("getBurned");
        }
    }
}
