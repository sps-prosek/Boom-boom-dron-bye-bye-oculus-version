using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    [Space]
    public float shootSpeed;
    public float gravityForce;
    public float bulletLifetime;

    public void Shoot()
    {   
        Quaternion tempRotation = shootPoint.transform.rotation;
        shootPoint.Rotate(Random.Range(0f, -0.8f), Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f));
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        ParabolicBullet bulletScript = bullet.GetComponent<ParabolicBullet>();
        if (bulletScript)
        {
            bulletScript.Initialize(shootPoint, shootSpeed, gravityForce);
        }
        Destroy(bullet, bulletLifetime);
        shootPoint.transform.rotation = tempRotation;
    }
}
