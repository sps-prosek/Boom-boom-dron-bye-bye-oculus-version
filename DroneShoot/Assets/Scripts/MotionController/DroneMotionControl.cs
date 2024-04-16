using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroneMotionControl : MonoBehaviour
{
    public float droneSpeed;
    public HPManager hpm;
    public GameObject bombPrefab;
    private bool loaded = true;
    
    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.right * Time.deltaTime * droneSpeed;
        if (transform.position.z < -200 && loaded){
            DropBomb();
            loaded = false;
        }
        if (transform.position.z < -800)
        {
            Destroy(gameObject);
        }
    }

    private void DropBomb(){
        GameObject bomb = Instantiate(bombPrefab);
        bomb.transform.position = gameObject.transform.position;
        bomb.GetComponent<BombImpact>().hpm = hpm;
        bomb.GetComponent<Rigidbody>().AddForce(bomb.gameObject.transform.forward * -10, ForceMode.Impulse);
    }
}
