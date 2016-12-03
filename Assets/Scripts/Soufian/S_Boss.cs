using UnityEngine;
using System.Collections;

public class S_Boss : S_Character
{
    [SerializeField] private AudioClip m_acAudioClip;

    /*protected override void Start ()
    {
        base.Start();
    }*/

    // Update is called once per frame
    /*protected override void Update ()
    {
        base.Update();

        Debug.Log("MaxLife : " + m_fMaxLife);
        Debug.Log("CurrentLife : " + m_fCurrentLife);

    }*/

    public override void Death()
    {
        base.Death();

        if (m_fCurrentLife <= 0f)
        {
            GameObject goFXDestruction;
            goFXDestruction = Instantiate(m_goFXDestruction, transform.position, m_goFXDestruction.transform.rotation) as GameObject;
            ++S_GameManager.Instance.iKilledEnemies;
            --S_GameManager.Instance.iCurrentAICpt;

            AudioSource.PlayClipAtPoint(m_acAudioClip, transform.position);

            Destroy(gameObject);
        }
    }

}
