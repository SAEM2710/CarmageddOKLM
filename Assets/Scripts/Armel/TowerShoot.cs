using UnityEngine;
using System.Collections;

public class TowerShoot : S_Character {

    // [SerializeField] protected float m_rotationSpeed;
    [SerializeField] protected Rigidbody carRb;

    private bool canShoot = true;

    // Use this for initialization
    protected override void Start () {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot(GameObject _goBullet)
    {
        base.Shoot(_goBullet);
        float x = Input.GetAxis("FireHorizontal");
        float y = -Input.GetAxis("FireVertical");

        if (x != 0.0f || y != 0.0f)
        {

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);

            if (m_fTime > m_fShootFrequence && canShoot)
            {
                m_fTime = 0f;
          
                GameObject goProjectile;
                goProjectile = Instantiate(_goBullet, m_v3PositionShoot, m_v3RotationShoot) as GameObject;
                goProjectile.GetComponent<S_Bullet>().bIsSpawnByPlayer = true;

                //Play Sound

                Rigidbody rProjectileRb = goProjectile.GetComponent<Rigidbody>();
                rProjectileRb.velocity = transform.TransformDirection(Vector3.forward * m_fShootForce) + carRb.velocity;
            }
    
            m_fTime += Time.deltaTime;
        }
    }

    public void CancelShoot()
    {
        canShoot = false;
    }

    public void ActiveShoot()
    {
        canShoot = true;
    }
}
