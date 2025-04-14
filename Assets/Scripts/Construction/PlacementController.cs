using System;
using UI;
using UnityEngine;

namespace Construction
{
    public class PlacementController : MonoBehaviour
    {
        public Action OnGrab;
        public Action OnPlace;
        
        [Header("FIND PLACEMENT OBJECT")]
        [SerializeField] private float _findDistance = 5f;
        [SerializeField] private UIController _uiController;
        [Header("PLACEMENT SETTINGS")]
        [SerializeField] private float _rotationStep = 45f;
        [SerializeField] private float _maxPlacementDistance = 5f;
        [SerializeField] private float _minPlacementDistance = 1f;
        [SerializeField] private LayerMask _ignorePlaceLayer;
        
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
                        OnGrab?.Invoke();
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
            
            if (Physics.Raycast(_ray, out RaycastHit hit, _maxPlacementDistance, ~_ignorePlaceLayer))
            {
                if (_currentPlacedObject.CheckValidLayers(hit.collider.gameObject.layer))
                {
                    Vector3 offset = Vector3.zero;

                    switch (_currentPlacedObject.PlacementType)
                    {
                        case PlacementType.Horizontal:
                            offset = Vector3.up * _currentPlacedObject.Size.y / 2;
                            break;
                        case PlacementType.Vertical:
                            offset = hit.normal * (_currentPlacedObject.Size.z / 2f);
                            break;
                    }
                    
                    _currentPlacedObject.transform.position = hit.point + offset;
                    _currentPlacedObject.SetValidColor();
                    
                    if (Input.GetMouseButtonDown(1))
                    {
                        if (!_currentPlacedObject.IsCollision)
                        {
                            Placement();
                        }
                    }
                }
                else
                {
                    SetDefaultPositionPlaceObject();
                }
            }
            else
            {
                SetDefaultPositionPlaceObject();
            }
        }

        private void SetDefaultPositionPlaceObject()
        {
            _currentPlacedObject.SetTransparentColor();
            Vector3 fallbackPos = _ray.origin + _ray.direction * _minPlacementDistance;
            _currentPlacedObject.transform.position = fallbackPos;
            _currentPlacedObject.transform.LookAt(_ray.origin);
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
            _currentPlacedObject.transform.SetParent(null);
            _currentPlacedObject.ExitConstruction();
            _currentPlacedObject = null;
            OnPlace?.Invoke();
        }
    }
}