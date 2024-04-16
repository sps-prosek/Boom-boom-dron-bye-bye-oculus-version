using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketImageColor : MonoBehaviour
{
    public Material[] missileImagesColors;
    private bool loaded = true;

    public void RocketLoaded()
    {
        if(!loaded)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.material = missileImagesColors[1];
            loaded = true;
        }
    }

    public void RocketLoading()
    {
        if(loaded)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.material = missileImagesColors[0];
            loaded = false;
        }
    }
}
