using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AimAssist : MonoBehaviour
{

    public GameObject shootingPoint;
    public GameObject drone;
    public DroneMotionControl dmc;
    private Vector3 v1;
    private Vector3 v2;
    public LineRenderer line;

    private double GetDistance(){
        v1 = shootingPoint.transform.position;
        v2 = drone.transform.position;   
        Vector3 difference = new Vector3(
        v1.x - v2.x,
        v1.y - v2.y,
        v1.z - v2.z);
        double distance = Math.Sqrt(
        Math.Pow(difference.x, 2f) +
        Math.Pow(difference.y, 2f) +
        Math.Pow(difference.z, 2f));
        return distance;
    }

    // Start is called before the first frame update
    void Start()
    {
        line.positionCount= 2;
    }

    // Update is called once per frame
    void Update()
    {
        float droneSpeed = dmc.droneSpeed;
        double distance = GetDistance();
        double deviation = ((Math.PI/180) * distance)*((distance-100)/100)*0.3;
        double travelDistance = distance / 200 * droneSpeed * -1;

        
        gameObject.transform.localPosition = new Vector3((float)travelDistance, (float)deviation, gameObject.transform.localPosition.z);

        line.SetPosition(0, new Vector3(0,0,0));
        line.SetPosition(1, gameObject.transform.localPosition);
        
    }
}
