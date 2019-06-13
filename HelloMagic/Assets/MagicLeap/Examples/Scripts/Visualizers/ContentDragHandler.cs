using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap
{
    /// Utility Component to allow users to drag persistent content around.
    [RequireComponent(typeof(Collider), typeof(MLPersistentBehavior))]
    public class ContentDragHandler : MonoBehaviour
    {
        #region Private Variables
        Vector3 _controllerPositionOffset;
        Quaternion _controllerOrientationOffset;
        ContentDragController _controllerDrag;
        MLPersistentBehavior _pointBehavior;

        bool _dragStarted = false;
        #endregion

        #region Unity Methods

        /// Set Up
        private void Start()
        {
            _pointBehavior = GetComponent<MLPersistentBehavior>();
        }

        /// Register for events when a ContentDragController enters the trigger area
        private void OnTriggerEnter(Collider other)
        {
            ContentDragController controllerDrag = other.GetComponent<ContentDragController>();
            if (controllerDrag == null)
            {
                return;
            }

            _controllerDrag = controllerDrag;
            _controllerDrag.OnBeginDrag += HandleBeginDrag;
            _controllerDrag.OnDrag += HandleDrag;
            _controllerDrag.OnEndDrag += HandleEndDrag;
        }

        /// Unregister for events when a ContentDragController leaves the trigger area
        private void OnTriggerExit(Collider other)
        {
            ContentDragController controllerDrag = other.GetComponent<ContentDragController>();
            if (controllerDrag == null)
            {
                return;
            }

            if (_controllerDrag == controllerDrag)
            {
                _controllerDrag.OnBeginDrag -= HandleBeginDrag;
                _controllerDrag.OnDrag -= HandleDrag;
                _controllerDrag.OnEndDrag -= HandleEndDrag;
                _controllerDrag = null;
            }
        }

        /// Unregister for events in case this component gets destroyed while being dragged
        private void OnDestroy()
        {
            if (_controllerDrag != null)
            {
                _controllerDrag.OnBeginDrag -= HandleBeginDrag;
                _controllerDrag.OnDrag -= HandleDrag;
                _controllerDrag.OnEndDrag -= HandleEndDrag;
                _controllerDrag = null;
            }
        }
        #endregion

        #region Event Handlers

        /// Set up offsets when dragging begins
        private void HandleBeginDrag()
        {
            Vector3 relativeDirection = transform.position - _controllerDrag.transform.position;
            _controllerPositionOffset = transform.InverseTransformDirection(relativeDirection);
            _controllerOrientationOffset = Quaternion.Inverse(_controllerDrag.transform.rotation) * transform.rotation;

            _dragStarted = true;
        }

        /// Update transform while dragging
        private void HandleDrag()
        {
            if (_dragStarted)
            {
                transform.position = _controllerDrag.transform.position + transform.TransformDirection(_controllerPositionOffset);
                transform.rotation = _controllerDrag.transform.rotation * _controllerOrientationOffset;
            }
        }

        /// Save binding when dragging ends
        private void HandleEndDrag()
        {
            _dragStarted = false;
            _pointBehavior.UpdateBinding();
        }
        #endregion
    }
}
