using UnityEngine;

namespace UI
{
        public class UIController : MonoBehaviour
        {
                [SerializeField] private GameObject _gripPointer;

                public void ShowGripPointer()
                { 
                        _gripPointer.gameObject.SetActive(true);
                }

                public void HideGripPointer()
                { 
                        _gripPointer.gameObject.SetActive(false);
                }
        }
}