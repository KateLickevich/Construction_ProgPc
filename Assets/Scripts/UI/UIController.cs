using UnityEngine;

namespace UI
{
        public class UIController : MonoBehaviour
        {
                [SerializeField] private GameObject _gripPointer;
                [SerializeField] private GameObject _returnObject;

                public void ShowGripPointer()
                {
                        _gripPointer.gameObject.SetActive(true);
                }

                public void HideGripPointer()
                {
                        _gripPointer.gameObject.SetActive(false);
                }

                public void ShowReturnObject()
                {
                        _returnObject.gameObject.SetActive(true);
                }

                public void HideReturnObject()
                {
                        _returnObject.gameObject.SetActive(false);
                }
        }
}