using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketTargeting : MonoBehaviour
{
    // Start is called before the first frame update[Header("REFERENCES")] 
        [SerializeField] private Rigidbody _rb;
        [SerializeField] public GameObject _target;
        [SerializeField] private GameObject _explosionPrefab;

        [Header("MOVEMENT")] 
        [SerializeField] private float _speed = 15;
        [SerializeField] private float _rotateSpeed = 95;

        [Header("PREDICTION")] 
        [SerializeField] private float _maxDistancePredict = 100;
        [SerializeField] private float _minDistancePredict = 5;
        [SerializeField] private float _maxTimePrediction = 5;
        private Vector3 _standardPrediction, _deviatedPrediction;

        [Header("DEVIATION")] 
        [SerializeField] private float _deviationAmount = 50;
        [SerializeField] private float _deviationSpeed = 2;

        [Header("KaBOOOM")]
        [SerializeField] private GameObject bigBoom;
        [SerializeField] private float _time;
        private float timeWithoutContatct = 0;
        private float timeToDestruct;

        private void FixedUpdate() {
            try
            {
                _rb.velocity = transform.forward * _speed;

                var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target.transform.position));

                PredictMovement(leadTimePercentage);

                AddDeviation(leadTimePercentage);

                RotateRocket();
            }
            catch (MissingReferenceException)
            {
                if (timeWithoutContatct == 0)
                {
                    timeWithoutContatct = Time.time;
                    timeToDestruct = timeWithoutContatct + _time;
                }

                if (timeToDestruct < Time.time)
                {
                    KaBoom();
                }
                
            }
        }

        private void PredictMovement(float leadTimePercentage) {
            var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);
            Rigidbody targetRb = _target.GetComponent<Rigidbody>();
            _standardPrediction = targetRb.position + targetRb.velocity * predictionTime;
        }

        private void AddDeviation(float leadTimePercentage) {
            var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
            
            var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

            _deviatedPrediction = _standardPrediction + predictionOffset;
        }

        private void RotateRocket() {
            var heading = _deviatedPrediction - transform.position;

            var rotation = Quaternion.LookRotation(heading);
            _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<ShootableDrone>())
            {
                collision.gameObject.GetComponent<ShootableDrone>().OnRocketHit();
                Destroy(gameObject);
            }
        }
        private void KaBoom(){
            Destroy(gameObject);
            GameObject imp = Instantiate(bigBoom, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(imp, 2f);
        }
}
