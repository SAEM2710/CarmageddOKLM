using UnityEngine;
using System.Collections;

public class S_Entity : MonoBehaviour 
{
    [SerializeField] protected GameObject m_goFXDestruction;

    // Use this for initialization
    protected virtual void Start () 
	{
        
    }
	
	// Update is called once per frame
	protected virtual void Update () 
	{
        Death();
    }

    public virtual void Death()
    {

    }
}
