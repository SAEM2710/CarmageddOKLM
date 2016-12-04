using UnityEngine;
using System.Collections;

public class VoicesOnPickUp : MonoBehaviour {

    [SerializeField] protected AudioClip[] voicesOnPickup;


    private AudioSource m_asAudio;
    private bool hasSpawn;

    // Use this for initialization
    void Start () {
        m_asAudio = GetComponent<AudioSource>();
        hasSpawn = true;
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void OnEnable()
    {
        int number = Random.Range(0, 3);
        if (number == 1)
        {
            int number2 = Random.Range(0, voicesOnPickup.Length);
            m_asAudio.PlayOneShot(voicesOnPickup[number2], 5.0f);
        }
    }

}
