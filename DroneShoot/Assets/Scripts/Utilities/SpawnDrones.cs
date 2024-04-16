using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDrones : MonoBehaviour
{
    public GameObject dronePrefab;
    public GameObject shootingPoint;
    public DroneShotCounter counter;
    public HPManager hpm;
    // Start is called before the first frame update
    public float droneSpawnRate;
    public bool spawnDronesSwitch = true;
    private float startTime;
    private float _nextSpawn;
    
    void Start()
    {
        startTime = Time.time;
        _nextSpawn = startTime + droneSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawn && spawnDronesSwitch)
        {
            GameObject newDrone = Instantiate(dronePrefab);
            newDrone.transform.position = new Vector3(Random.Range(-200, 200), Random.Range(100, 500), Random.Range(1000, 1800));
            Vector3 targetDirection = new Vector3(0,0,0) - newDrone.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(-1*newDrone.transform.right, targetDirection, 0, 0);
            newDrone.transform.rotation = Quaternion.LookRotation(newDirection);
            newDrone.GetComponent<ShootableDrone>().counter = counter;
            newDrone.GetComponent<DroneMotionControl>().hpm = hpm;
            GameObject mySphere = newDrone.gameObject.transform.Find("Sphere").gameObject;
            mySphere.GetComponent<AimAssist>().shootingPoint = shootingPoint;
            _nextSpawn += droneSpawnRate;
        }
    }
}
