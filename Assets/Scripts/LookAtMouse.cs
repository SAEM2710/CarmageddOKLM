using UnityEngine;
using System.Collections;

public class LookAtMouse : SEntity
{
    private Rigidbody m_rRigidbody;

    // speed is the rate at which the object will rotate
    public float speed;

    void Start()
    {
        m_rRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        ShootTest();
    }

    private void ShootTest()
    {
        base.Shoot();

        if (Input.GetButton("Fire1"))
        {
            if (m_fTime > m_fShootFrequence)
            {
                m_fTime = 0f;

                GameObject goProjectile;
                goProjectile = Instantiate(m_goProjectile, m_v3PositionShoot, m_v3RotationShoot) as GameObject;
                goProjectile.GetComponent<SProjectile>().bIsSpawnByPlayer = true;

                //Play Sound

                Rigidbody rProjectileRb = goProjectile.GetComponent<Rigidbody>();
                //Debug.Log(m_rRigidbody);
                rProjectileRb.velocity = transform.TransformDirection(Vector3.forward * m_fShootForce); //+ m_rRigidbody.velocity;
            }
            m_fTime += Time.deltaTime;
        }
    }
}