using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionControl : MonoBehaviour
{
    public Animator animator;
    public float rotationSpeed;
    public float elevationSpeed;
    private float AnimStateTime;
    private float AnimProgress = 0f;
    public float rotationAcceleration;
    private float currentRotationSpeed;
    public float barrelRotationSpeed;
    public float barrelRotationAcceleration;
    private float barrelCurrentRotationSpeed;
    public GameObject barrel;
    public ShootingManager sm;
    public int magazine;
    public float reloadTime;
    public GameObject[] ammoLights;
    public AudioSource aS;
    public AudioSource aSEcho;
    public AudioSource aSMRotation;
    public AudioSource aSMClicking;
    private int _fullMag;
    private float _lightToMag;
    private float _timeToReload = 0;
    private bool _lastShot = false;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = rotationSpeed / 10;
        elevationSpeed = elevationSpeed / 1000;
        rotationAcceleration = rotationAcceleration / 100;
        barrelRotationAcceleration = barrelRotationAcceleration / 100;
        _fullMag = magazine;
        _lightToMag = magazine / ammoLights.Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Elevation mechanism
        if (Input.GetKey(KeyCode.UpArrow))
        {
            AnimProgress += elevationSpeed;

            if (AnimProgress > 1)
            {
                AnimProgress = 1;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            AnimProgress -= elevationSpeed;

            if (AnimProgress <0)
            {
                AnimProgress = 0;
            }
        }
        animator.SetFloat ("AnimStateTime", AnimProgress, 1, 10 * Time.deltaTime);
        // End of elevation ----------------------------------------------------------------
        
        // Rotation mechanism
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotationSpeed -= rotationAcceleration;
            if (currentRotationSpeed < -1)
            {
                currentRotationSpeed = -1;
            }
        }else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotationSpeed += rotationAcceleration;
            if (currentRotationSpeed >1)
            {
                currentRotationSpeed = 1;
            }
        }else
        {
            if (currentRotationSpeed < 0)
            {
                currentRotationSpeed += rotationAcceleration;
            }
            
            if (currentRotationSpeed > 0)
            {
                currentRotationSpeed -= rotationAcceleration;
            }
           
            if ((currentRotationSpeed < 0.1 && currentRotationSpeed > 0) || (currentRotationSpeed > -0.1 && currentRotationSpeed < 0))
            {
                currentRotationSpeed = 0;
            }
        }
        transform.Rotate (0,rotationSpeed * currentRotationSpeed,0);
        // End of turret control --------------------------------------------

        if (Input.GetKey(KeyCode.Space))
        {
            barrelCurrentRotationSpeed += barrelRotationAcceleration;
            if (barrelCurrentRotationSpeed > 1)
            {
                barrelCurrentRotationSpeed = 1;
            }
            if (barrelCurrentRotationSpeed == 1 && magazine > 0 && _timeToReload == 0)
            {
                aS.volume = 1;
                _lastShot = true;
                sm.Shoot();
                magazine--;
                HandleMagazineLights();
                if (magazine == 0 && _timeToReload == 0)
                {
                    _timeToReload = Time.time + reloadTime;
                }
            }else
            {
                _lastShot = false;
                aS.volume = 0;
            }        
        }else
        {        
            aS.volume = 0;    
            if (barrelCurrentRotationSpeed > 0)
            {
                barrelCurrentRotationSpeed -= barrelRotationAcceleration;
            }
           
            if ((barrelCurrentRotationSpeed < 0.1 && barrelCurrentRotationSpeed > 0))
            {
                barrelCurrentRotationSpeed = 0;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) && _lastShot){
            aSEcho.Play();
            _lastShot = false;
        }
        aSMRotation.pitch = 2 * barrelCurrentRotationSpeed;
        aSMClicking.pitch = 2 * barrelCurrentRotationSpeed;
        if (aSMRotation.pitch > 1)
        {
            aSMRotation.pitch = 1;
        }
        if (aSMClicking.pitch > 1.2f)
        {
            aSMClicking.pitch = 1.2f;
        }
        barrel.transform.Rotate (0, 0, barrelRotationSpeed * barrelCurrentRotationSpeed);
        HandleMagazineLights();

        if (magazine == 0 && _timeToReload == 0)
        {
            _timeToReload = Time.time + reloadTime;
        }
        if (_timeToReload != 0 && _timeToReload < Time.time)
        {
            _timeToReload = 0;
            magazine = _fullMag;
        }        
    }

    private void HandleMagazineLights(){
        int ammoShot = _fullMag - magazine;
        float emptySlots = ammoShot / _lightToMag;
        for (int i = 0; i < ammoLights.Length; i++)
        {
            if (i < (int)emptySlots)
            {
                ammoLights[i].GetComponent<AmmoLightController>().ShutDown();
            }else{
                ammoLights[i].GetComponent<AmmoLightController>().TurnOn();
            }            
        }
    }
}
