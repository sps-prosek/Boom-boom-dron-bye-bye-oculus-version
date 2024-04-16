using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Material[] lightMat = new Material[2];
    private bool status = false;
    public void TurnOn(){
        if (!status)
        {
            MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
            mr.material = lightMat[0];
            status = true;
        }
    }
    public void TurnOff(){
        if (status)
        {
            MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
            mr.material = lightMat[1];
            status = false;
        }
    }
}
