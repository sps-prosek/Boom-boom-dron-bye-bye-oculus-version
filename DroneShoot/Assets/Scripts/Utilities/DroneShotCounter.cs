using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DroneShotCounter : MonoBehaviour
{
    public int numberOFDestroyedDrones = 0;
    public TextMeshPro text;
    
    public void AddMore(){
        
        numberOFDestroyedDrones++;
        text.SetText(numberOFDestroyedDrones.ToString());
    }
}
