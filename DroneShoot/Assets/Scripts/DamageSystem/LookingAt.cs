using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookingAt : MonoBehaviour
{
    public GameObject lookingAt;
    public GameObject rocketPrefab;
    public GameObject[] startingPositions;
    public GameObject lockedLight;
    public GameObject redyLight;
    public float realoadTime;
    public GameObject[] missileImages;
    private int lastPosition = 0;
    private float[] _reloadTimes;
    void Start(){
        _reloadTimes = new float[startingPositions.Length];
        for (int i = 0; i < _reloadTimes.Length; i++)
        {
            _reloadTimes[i] = 0;
        }
    }
    void Update()
    {
        Ray gazeRay = new Ray(transform.position, transform.rotation * Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(gazeRay, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.TryGetComponent<LockableObject>(out LockableObject ob))
            {
                lookingAt = ob.droneBody;
                if (_reloadTimes[lastPosition] < Time.time)
                {
                    lockedLight.GetComponent<LightController>().TurnOn();
                }
            }else{
                lookingAt = null;
                lockedLight.GetComponent<LightController>().TurnOff();
            }
        }else{
            lookingAt = null;
            lockedLight.GetComponent<LightController>().TurnOff();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && lookingAt != null && _reloadTimes[lastPosition] < Time.time)
        {
            GameObject rocket = Instantiate(rocketPrefab, startingPositions[lastPosition].gameObject.transform.position, startingPositions[lastPosition].gameObject.transform.rotation);
            rocket.GetComponent<RocketTargeting>()._target = lookingAt;
            _reloadTimes[lastPosition] = Time.time + realoadTime;
            lastPosition++;
            if (lastPosition >= startingPositions.Length)
            {
                lastPosition = 0;
            }
        }
        UpdateRocketLights();
    }
    private void UpdateRocketLights()
    {
        for(int i = 0; i < startingPositions.Length; i++)
        {
            if(_reloadTimes[i] == 0 || _reloadTimes[i] < Time.time)
            {
                missileImages[i].GetComponent<RocketImageColor>().RocketLoaded();
            }else
            {
                missileImages[i].GetComponent<RocketImageColor>().RocketLoading();
            }
        }
    }
}
