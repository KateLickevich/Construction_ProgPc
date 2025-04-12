using System;
using UI;
using UnityEngine;

namespace Construction
{
    public class PlacementController : MonoBehaviour
    {
        public Action<Transform> OnGrab;
        
        [Header("FIND PLACEMENT OBJECT")]
        [SerializeField] private float _findDistance = 5f;
        [SerializeField] private UIController _uiController;
        [Header("PLACEMENT SETTINGS")]
        [SerializeField] private float _rotationStep = 45f;

        private PlacedObject _currentPlacedObject;
        private Ray _ray;
        private float _targetPlacedObjectRotationY = 0f;

        private void Awake()
        {
            _uiController.HideGripPointer();
        }

        public void CheckFront()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            FindPlacedObject();
        }

        private void FindPlacedObject()
        {
            _uiController.HideGripPointer();
            Debug.DrawRay(_ray.origin, _ray.direction * _findDistance, Color.red);
            if (Physics.Raycast(_ray, out RaycastHit hit, _findDistance))
            {
                if (hit.collider.gameObject.TryGetComponent(out IActivateConstruction obj))
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        _currentPlacedObject = obj as PlacedObject;
                        obj.EnterConstruction();
                        OnGrab?.Invoke(_currentPlacedObject.transform);
                    }
                    else
                    {
                        _uiController.ShowGripPointer();
                    }
                }
            }
        }

        public void TryPlacementObject()
        {
            RotatePlacedObject();
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void RotatePlacedObject()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            
            if (scroll > 0f)
            {
                _targetPlacedObjectRotationY += _rotationStep;
            }
            else if (scroll < 0f)
            {
                _targetPlacedObjectRotationY -= _rotationStep;
            }
            
            Quaternion targetRotation = Quaternion.Euler(0, _targetPlacedObjectRotationY, 0);
            _currentPlacedObject.transform.localRotation = targetRotation;
        }
        
        public void Placement()
        {
            _currentPlacedObject.ExitConstruction();
            
            switch (_currentPlacedObject.PlacementType)
            {
                case PlacementType.Horizontal:
                    break;
                case PlacementType.Wall:
                    break;
            }
        }
    }
}