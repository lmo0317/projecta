using UnityEngine;

namespace TopDownShooter
{
    public class TopDownCamera : MonoBehaviour
    {
        public Transform Target;

        public float SeeForward;

        public float RotationSpeed = 3;

        public Vector3 Offset;

        // The speed with which the camera will be following.
        public float Smoothing = 5f;

        // The initial offset from the target.

        private bool _rotateToLeft;
        private bool _rotateToRight;


        private void Start()
        {
            //_offset = transform.position - Target.position;
        }

        private void Update()
        {
            //rotate camera input
            _rotateToLeft = Input.GetKey(KeyCode.E);
            _rotateToRight = Input.GetKey(KeyCode.Q);
        }

        private void FixedUpdate()
        {
            var targetCamPos = Target.position + Offset + Target.forward * SeeForward;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, Smoothing * Time.deltaTime);
            if (_rotateToLeft) RotateToLeft();
            if (_rotateToRight) RotateToRight();
        }

        private void RotateToLeft()
        {
            transform.Rotate(Vector3.up * RotationSpeed, Space.World);
        }

        private void RotateToRight()
        {
            transform.Rotate(Vector3.up * -RotationSpeed, Space.World);
        }
    }
}