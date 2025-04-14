using System.Collections.Generic;
using UnityEngine;

namespace Construction
{
    public class PlacedObject : MonoBehaviour,  IActivateConstruction
    {
        [SerializeField] private PlacementType _placementType;
        [SerializeField] private LayerMask _validLayers;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Collider _collider;
        [SerializeField] private Color _validColor;
        [SerializeField] private Color _invalidColor;
        [SerializeField][Range(0f, 1f)] private float _enterConstructionAlphaValue;

        private HashSet<Collider> _invalidColliders = new();
        private Color _defaultColor;
        private bool _isCollision;

        public PlacementType PlacementType => _placementType;
        public Vector3 Size => _meshRenderer.bounds.size;
        public bool IsCollision => _isCollision;

        private void Start()
        {
            _defaultColor = _meshRenderer.material.color;
        }

        public void EnterConstruction()
        {
            SetTransparentColor();
            _collider.isTrigger = true;
        }

        public void ExitConstruction()
        {
            _meshRenderer.material.color = _defaultColor;
            _collider.isTrigger = false;
        }

        public void SetValidColor()
        {
            if (_isCollision)
            {
                _meshRenderer.material.color = SetColorAlpha(_invalidColor, _enterConstructionAlphaValue);
            }
            else
            {
                _meshRenderer.material.color = SetColorAlpha(_validColor, _enterConstructionAlphaValue);
            }
        }
        
        public void SetTransparentColor()
        {
            _meshRenderer.material.color = SetColorAlpha(_defaultColor, _enterConstructionAlphaValue);
        }
        
        public bool CheckValidLayers(int layer)
        {
            return  ((1 << layer) & _validLayers) != 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!CheckValidLayers(other.gameObject.layer))
            {
                _invalidColliders.Add(other);
                _isCollision = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!CheckValidLayers(other.gameObject.layer))
            {
                _invalidColliders.Remove(other);
            }

            if (_invalidColliders.Count == 0)
            {
                _isCollision = false;
            }
        }

        private Color SetColorAlpha(Color currentColor, float alpha)
        {
            Color color = currentColor;
            color.a = alpha;
            return color;
        }
    }
}