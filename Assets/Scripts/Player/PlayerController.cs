using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("MOVEMENT CONFIG")] 
        [SerializeField] private CharacterController controller;
        [SerializeField] private float moveSpeed;
        [Header("ROTATION CONFIG")]
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float _minRotation = -90f;
        [SerializeField] private float _maxRotation = 90f;

        [Header("PLACEMENT OBJECT")] 
        [SerializeField] private Transform _placementObjectPoint;
        [FormerlySerializedAs("_placementObjectCollider")] [SerializeField] private Collider _placedObjectCollider;

        private float verticalRotation = 0f;

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
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, _minRotation, _maxRotation);

            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        public void TakePlacedObject(Transform placementObject)
        {
            placementObject.SetParent(transform);
            placementObject.SetLocalPositionAndRotation(_placementObjectPoint.localPosition, Quaternion.Euler(Vector3.zero));
        }

        public void ObjectPlacement()
        {
            _placedObjectCollider.enabled = false;
        }
    }
}
