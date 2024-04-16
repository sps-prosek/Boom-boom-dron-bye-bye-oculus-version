using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotate : MonoBehaviour
{
    public GameObject copyObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 newRotation = new Vector3(copyObject.transform.eulerAngles.x, copyObject.transform.eulerAngles.y, copyObject.transform.eulerAngles.z);
        gameObject.transform.eulerAngles = newRotation;
    }
}
