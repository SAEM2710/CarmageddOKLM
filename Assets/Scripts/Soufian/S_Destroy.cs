using UnityEngine;
using System.Collections;

public class S_Destroy : MonoBehaviour
{
    [SerializeField] private float m_fSecondsBeforeDestroy;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, m_fSecondsBeforeDestroy);
	}
}
