using UnityEngine;
using System.Collections;

public class S_Boss : S_Character
{
    [SerializeField] private AudioClip m_acAudioClip;
    [SerializeField] private GameObject m_goShield;
    [SerializeField] private float m_fMaxShieldValue;
    [SerializeField] private Renderer m_rRenderer;

    private bool m_bShieldActivated;
    private float m_fCurrentShieldValue;
    private Color m_cStartColor;

    protected override void Start ()
    {
        base.Start();
        m_bShieldActivated = true;
        m_fCurrentShieldValue = m_fMaxShieldValue;
        m_cStartColor = m_rRenderer.material.GetColor("_Solid_Color");
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();

        DesactivateShield();
    }

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

    public override void LoseLife(float _fDamage)
    {
        if (m_bShieldActivated)
        {
            m_fCurrentShieldValue -= _fDamage;
        }
        else
        {
            m_fCurrentLife -= _fDamage;
        }
    }

    private void DesactivateShield()
    {
        if (m_bShieldActivated)
        {
            if (m_fCurrentShieldValue <= 0)
            {
                m_bShieldActivated = false;
                StartCoroutine(DestroyShield());
            }
        }
    }

    public IEnumerator ShieldFeedback(/*Renderer _rRenderer*/)
    {
        Color cNewColor = new Color(1.0f, 0f, 0f);
        //yield return new WaitForSeconds(1.0f);
        if (m_rRenderer.material.GetFloat("_Brightness") <= 1.0f)
        {
            m_rRenderer.material.SetFloat("_Brightness", 1.5f);
            m_rRenderer.material.SetFloat("_Intensity", 1.5f);
            m_rRenderer.material.SetColor("_Solid_Color", cNewColor);
            yield return new WaitForSeconds(0.19f);
        }
        else if (m_rRenderer.material.GetFloat("_Brightness") >= 1.5f)
        {
            m_rRenderer.material.SetFloat("_Brightness", 1.0f);
            m_rRenderer.material.SetFloat("_Intensity", 1.0f);
            m_rRenderer.material.SetColor("_Solid_Color", m_cStartColor);
            yield return new WaitForSeconds(0.19f);
        }
    }

    public IEnumerator DestroyShield()
    {
        float fCpt = 0f;
        while (fCpt > 1.0f)
        {
            fCpt -= 0.005f;
            m_rRenderer.material.SetFloat("_Intensity", fCpt);
            yield return new WaitForSeconds(0.005f);
        }
        m_goShield.SetActive(false);
    }
}
