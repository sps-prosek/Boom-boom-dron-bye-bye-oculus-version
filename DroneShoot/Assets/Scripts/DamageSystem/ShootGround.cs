using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootGround : ShootableObject
{
    public GameObject particlePrefab;
    public override void OnHit(RaycastHit hit)
    {
        GameObject imp = Instantiate(particlePrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(imp, 0.5f);
    }
}
