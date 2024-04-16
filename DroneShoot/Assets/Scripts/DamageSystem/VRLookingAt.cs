using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRLookingAt : MonoBehaviour
{
    public GameObject lookingAt;
    public GameObject rocketPrefab;
    public GameObject[] startingPositions;
    public GameObject lockedLight;
    public GameObject redyLight;
    public float realoadTime;
    public InputActionReference triggerInput;
    public InputActionReference warmUpKey;
    public float warmUpTime = 3f;
    public float flashSpeed = 0.3f;
    public GameObject[] missileImages;
    private int lastPosition = 0;
    private float[] _reloadTimes;
    private float warmUpPrepTime;
    private bool ligthOnOff = false;
    private float nextFlash = 0;

    void Start(){
        _reloadTimes = new float[startingPositions.Length];
        for (int i = 0; i < _reloadTimes.Length; i++)
        {
            _reloadTimes[i] = 0;
        }
        warmUpPrepTime = 0;
    }
    void Update()
    {
        Ray gazeRay = new Ray(transform.position, transform.rotation * Vector3.forward);
        RaycastHit hit;
        if(warmUpPrepTime == 0 && warmUpKey.action.ReadValue<float>() == 1)
        {
            warmUpPrepTime = Time.time + warmUpTime;
            redyLight.GetComponent<WarmUpLightControl>().TurnOn();
            ligthOnOff = true;
            nextFlash = Time.time + flashSpeed;

        }
        if(warmUpPrepTime != 0 && warmUpKey.action.ReadValue<float>() == 0){
            warmUpPrepTime = 0;
            nextFlash = 0;
            ligthOnOff = false;
            redyLight.GetComponent<WarmUpLightControl>().TurnOff();
        }
        if (Physics.Raycast(gazeRay, out hit, Mathf.Infinity))
        {
            if(warmUpPrepTime != 0 && warmUpPrepTime < Time.time && warmUpKey.action.ReadValue<float>() == 1){
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
                ligthOnOff = true;
                redyLight.GetComponent<WarmUpLightControl>().TurnOn();

            }
            else{
                lookingAt = null;
                lockedLight.GetComponent<LightController>().TurnOff();
            }
        }else{
            lookingAt = null;
            lockedLight.GetComponent<LightController>().TurnOff();
        }
        if(warmUpPrepTime != 0 && warmUpPrepTime > Time.time && warmUpKey.action.ReadValue<float>() == 1){
            if(nextFlash != 0 && nextFlash < Time.time){
                if(ligthOnOff){
                    redyLight.GetComponent<WarmUpLightControl>().TurnOff();
                    ligthOnOff = false;
                }else{
                    redyLight.GetComponent<WarmUpLightControl>().TurnOn();
                    ligthOnOff = true;
                }
                nextFlash = Time.time + flashSpeed;
            }
        }
        if(warmUpPrepTime < Time.time && warmUpKey.action.ReadValue<float>() == 1){
            ligthOnOff = true;
            redyLight.GetComponent<WarmUpLightControl>().TurnOn();
        }

        if (triggerInput.action.ReadValue<float>() == 1 && lookingAt != null && _reloadTimes[lastPosition] < Time.time)
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
            if(_reloadTimes[i] == 0 || _reloadTimes[i] > Time.time)
            {
                missileImages[i].GetComponent<RocketImageColor>().RocketLoaded();
            }else
            {
                missileImages[i].GetComponent<RocketImageColor>().RocketLoading();
            }
        }
    }
}
