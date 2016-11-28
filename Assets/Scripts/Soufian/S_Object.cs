using UnityEngine;
using System.Collections;

public class S_Object : S_Entity
{
    [SerializeField] protected float m_fSecondsBeforeDestruction;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        Destroy(gameObject, m_fSecondsBeforeDestruction);
    }
}
