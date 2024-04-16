using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableDrone : ShootableObject
{
    // Start is called before the first frame update
    public GameObject bigBoom;
    public DroneShotCounter counter;
    public override void OnHit(RaycastHit hit)    
    {
        Destroy(gameObject);
        counter.AddMore();
        GameObject imp = Instantiate(bigBoom, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(imp, 14f);
    }
    public void OnRocketHit(){
        Destroy(gameObject);
        counter.AddMore();
        GameObject imp = Instantiate(bigBoom, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(imp, 14f);
    }
}
