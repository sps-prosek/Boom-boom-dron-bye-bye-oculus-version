using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombImpact : MonoBehaviour
{
    public GameObject explosion;
    public HPManager hpm;
    private float startTime;
    void Start(){
        startTime = Time.time;
    }
    void Update(){
        if (gameObject.transform.position.y < -80)
        {
            Destroy(gameObject);
            GameObject imp = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            hpm.GetHit();
            Destroy(imp, 2f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time >= startTime + 0.5f)
        {
            Destroy(gameObject);
            GameObject imp = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            hpm.GetHit();
            Destroy(imp, 2f);
        }
    }
}