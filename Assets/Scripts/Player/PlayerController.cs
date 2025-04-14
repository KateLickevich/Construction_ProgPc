using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("MOVEMENT CONFIG")] 
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _moveSpeed;
        [Header("ROTATION CONFIG")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _mouseSensitivity;
        [SerializeField] private float _minRotation = -90f;
        [SerializeField] private float _maxRotation = 90f;
        
        private float _verticalRotation = 0f;

        void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = transform.right * h + transform.forward * v;
            _controller.Move(move * _moveSpeed * Time.deltaTime);
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, _minRotation, _maxRotation);

            _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
