using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLightController : MonoBehaviour
{
    public Material[] amoLightsMats = new Material[2];
    private bool status = true;

    public void ShutDown(){
        if (status)
        {
            gameObject.GetComponent<MeshRenderer>().material = amoLightsMats[1];
            status = false;
        }
    }

    public void TurnOn(){
        if (!status)
        {
            gameObject.GetComponent<MeshRenderer>().material = amoLightsMats[0];
            status = true;
        }
    }
}
