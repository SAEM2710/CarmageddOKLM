using UnityEngine;
using System.Collections;

public class CarCrashSounds : MonoBehaviour {

    [SerializeField] protected AudioClip crashSound1;
    [SerializeField] protected AudioClip crashSound2;
    [SerializeField] protected AudioClip crashSound3;

    private AudioClip[] crashes;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        crashes = new AudioClip[]
        {
            crashSound1, crashSound2, crashSound3
        };
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision _cCollision)
    {
        if (_cCollision.gameObject.CompareTag("Obstacle"))
        {
            int number = Random.Range(0, crashes.Length);
            audio.PlayOneShot(crashes[number]);
        }
    }
}
