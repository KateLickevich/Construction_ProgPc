using UnityEngine;

namespace Construction
{
    public class PlacedObject : MonoBehaviour,  IActivateConstruction
    {
        [SerializeField] private PlacementType _placementType;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Color _possiblePlacementColor;
        [SerializeField] private Color _impossiblePlacementColor;
        [SerializeField][Range(0f, 1f)] private float _enterConstructionAlphaValue;

        private Color _defaultColor;
        
        public PlacementType PlacementType => _placementType;

        private void Start()
        {
            _defaultColor = _meshRenderer.material.color;
        }

        public void EnterConstruction()
        {
            Color color = _defaultColor;
            color.a = _enterConstructionAlphaValue;
            _meshRenderer.material.color = color;
        }

        public void ExitConstruction()
        {
            _meshRenderer.material.color = _defaultColor;
        }

     
    }
}