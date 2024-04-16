using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public GameObject[] HPPoints;
    public ScoreAndMenuManager samm;
    public SpawnDrones sd;
    private int _HP = 8;
    
    public void GetHit(){
        _HP--;
        if (_HP >= 0){
            Destroy(HPPoints[_HP]);
        }      
        if(_HP == 0) 
        {
            samm.KillPlayer();
            sd.spawnDronesSwitch = false;
        }
    }
}
