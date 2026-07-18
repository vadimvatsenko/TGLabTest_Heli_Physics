using System;
using UnityEngine;

namespace CameraSystem
{
    public class BaseCamera : MonoBehaviour
    {
        [Header("Camera Properties")] [SerializeField]
        private float height = 5;

        [SerializeField] private float minGroundHeight = 4f;
        [SerializeField] private float minDistance = 4f;
        [SerializeField] private float maxDistance = 8f;
        [SerializeField] private float catchUpModifirier = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float minVelocityForOrient = 5f;
        [SerializeField] private bool unUseHeliSelfRotate = true;
        [Space] [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform lookAt;

        private float _finalAngle;
        private Vector3 _wantedDir;
        private float _finalHeight;

        private Vector3 _wantedPos;
        private Vector3 _targetFlatFwd;

        public float MinDistance
        {
            get => minDistance;
            set => minDistance = value;
        }

        public float MaxDistance
        {
            get => maxDistance;
            set => maxDistance = value;
        }

        private void LateUpdate()
        {
            UpdateCamera();
        }

        private void UpdateCamera()
        {

            Vector3 dirToTarget = transform.position - rb.position;
            dirToTarget.y = 0;
            Vector3 normalizedDir = dirToTarget.normalized;
            _wantedDir = normalizedDir;
            Debug.DrawRay(rb.position, _wantedDir, Color.green);


            float angleToFwd = Vector3.SignedAngle(normalizedDir, _targetFlatFwd, Vector3.up);

            float wantedAngle = 0f;
            if (unUseHeliSelfRotate)
            {
                if (rb.linearVelocity.magnitude > minVelocityForOrient)
                {
                    wantedAngle = angleToFwd * Time.fixedDeltaTime;
                }
            }
            else
            {
                wantedAngle = angleToFwd * Time.fixedDeltaTime;
            }

            _finalAngle = Mathf.Lerp(_finalAngle, wantedAngle, Time.fixedDeltaTime * rotationSpeed);
            _wantedDir = Quaternion.AngleAxis(_finalAngle, Vector3.up) * _wantedDir;

            // re-position camera
            _wantedPos = rb.position + (_wantedDir * dirToTarget.magnitude);
            float currentMagnitude = dirToTarget.magnitude;

            if (currentMagnitude < minDistance)
            {
                float delta = minDistance - currentMagnitude;
                _wantedPos += _wantedDir * (delta * Time.fixedDeltaTime * catchUpModifirier);
            }
            else if (currentMagnitude > maxDistance)
            {
                float delta = currentMagnitude - maxDistance;
                _wantedPos -= _wantedDir * (delta * Time.fixedDeltaTime * catchUpModifirier);
            }

            // Take into account the height from the ground
            float wantedHeight = height;
            RaycastHit hit;
            Ray groundRay = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(groundRay, out hit, 100f))
            {
                if (hit.collider.gameObject.CompareTag("Ground") && hit.distance < minGroundHeight)
                {
                    wantedHeight = minGroundHeight - hit.distance;
                }
            }

            _finalHeight = Mathf.Lerp(_finalHeight, wantedHeight, Time.fixedDeltaTime * 2f);
            
            transform.position = _wantedPos + (Vector3.up * _finalHeight);
            transform.LookAt(lookAt);
        }
    }
}
