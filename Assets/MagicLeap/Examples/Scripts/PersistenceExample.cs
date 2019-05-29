// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
//
// Copyright (c) 2019 Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Creator Agreement, located
// here: https://id.magicleap.com/creator-terms
//
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap
{
    /// <summary>
    /// Demonstrates how to persist objects dynamically by interfacing with
    /// the MLPersistence API. This facilitates restoration of existing
    /// and creation of new persistent points.
    /// </summary>
    [RequireComponent(typeof(PrivilegeRequester))]
    public class PersistenceExample : MonoBehaviour
    {
        #region Private Variables
        [SerializeField, Tooltip("Content to create")]
        GameObject _content = null;
        List<MLPersistentBehavior> _pointBehaviors = new List<MLPersistentBehavior>();

        //[SerializeField, Tooltip("Destroyed content effect")]
        //GameObject _destroyedContentEffect = null;

       [SerializeField, Tooltip("Text to count restored objects")]
        Text _countRestoredText = null;
        string _countRestoredTextFormat;
        int _countRestoredGood = 0;
        int _countRestoredBad = 0;

        [SerializeField, Tooltip("Text to count created objects")]
        Text _countCreatedText = null;
        string _countCreatedTextFormat;
        int _countCreatedGood = 0;
        int _countCreatedBad = 0;

        [SerializeField, Tooltip("Controller")]
        ControllerConnectionHandler _controller = null;

        [SerializeField, Tooltip("Distance in front of Controller to create content")]
        float _distance = 0.2f;

        PrivilegeRequester _privilegeRequester;
        #endregion 

        #region Unity Methods
       
        void Awake()
        {
            if (_content == null || _content.GetComponent<MLPersistentBehavior>() == null)
            {
                Debug.LogError("Error: PersistenceExample._content is not set or is missing MLPersistentBehavior behavior, disabling script.");
                enabled = false;
                return;
            }

           
            _countCreatedTextFormat = _countCreatedText.text;
            _countCreatedText.text = string.Format(_countCreatedTextFormat, _countCreatedGood, _countCreatedBad);

            if (_countRestoredText == null)
            {
                Debug.LogError("Error: PersistenceExample._countRestoredText is not set, disabling script.");
                enabled = false;
                return;
            }
            _countRestoredTextFormat = _countRestoredText.text;
            _countRestoredText.text = string.Format(_countRestoredTextFormat, _countRestoredGood, _countRestoredBad);

            _privilegeRequester = GetComponent<PrivilegeRequester>();
            _privilegeRequester.OnPrivilegesDone += HandlePrivilegesDone;
        }

        void OnDestroy()
        {
            foreach (MLPersistentBehavior pointBehavior in _pointBehaviors)
            {
                if (pointBehavior != null)
                {
                    RemoveContentListeners(pointBehavior);
                    Destroy(pointBehavior.gameObject);
                }
            }

            if (MLPersistentCoordinateFrames.IsStarted)
            {
                MLPersistentCoordinateFrames.Stop();
            }

            if (MLPersistentStore.IsStarted)
            {
                MLPersistentStore.Stop();
            }

            if (_privilegeRequester != null)
            {
                _privilegeRequester.OnPrivilegesDone -= HandlePrivilegesDone;
            }

            MLInput.OnControllerButtonDown -= HandleControllerButtonDown;
        }
        #endregion

        #region Event Handlers
      
        void HandleControllerButtonDown(byte controllerId, MLInputControllerButton button)
        {
            if (!_controller.IsControllerValid(controllerId))
            {
                return;
            }

            if (button == MLInputControllerButton.Bumper)
            {
                Vector3 position = _controller.transform.position + _controller.transform.forward * _distance;
                CreateContent(position, _controller.transform.rotation);
            }
            else if (button == MLInputControllerButton.HomeTap)
            {
            }
        }

        void HandlePrivilegesDone(MLResult result)
        {
            _privilegeRequester.OnPrivilegesDone -= HandlePrivilegesDone;
            if (!result.IsOk)
            {
                if (result.Code == MLResultCode.PrivilegeDenied)
                {
                    Instantiate(Resources.Load("PrivilegeDeniedError"));
                }

                Debug.LogErrorFormat("Error: PersistenceExample failed to get requested privileges, disabling script. Reason: {0}", result);
                enabled = false;
                return;
            }

            result = MLPersistentStore.Start();
            if (!result.IsOk)
            {
                if (result.Code == MLResultCode.PrivilegeDenied)
                {
                    Instantiate(Resources.Load("PrivilegeDeniedError"));
                }

                Debug.LogErrorFormat("Error: PersistenceExample failed starting MLPersistentStore, disabling script. Reason: {0}", result);
                enabled = false;
                return;
            }

            result = MLPersistentCoordinateFrames.Start();
            if (!result.IsOk)
            {
                if (result.Code == MLResultCode.PrivilegeDenied)
                {
                    Instantiate(Resources.Load("PrivilegeDeniedError"));
                }

                MLPersistentStore.Stop();
                Debug.LogErrorFormat("Error: PersistenceExample failed starting MLPersistentCoordinateFrames, disabling script. Reason: {0}", result);
                enabled = false;
                return;
            }

            if (MLPersistentCoordinateFrames.IsReady)
            {
                PerformStartup();
            }
            else
            {
                MLPersistentCoordinateFrames.OnInitialized += HandleInitialized;
            }
        }

        void HandleInitialized(MLResult status)
        {
            MLPersistentCoordinateFrames.OnInitialized -= HandleInitialized;

            if (status.IsOk)
            {
                PerformStartup();
            }
            else
            {
                Debug.LogErrorFormat("Error: MLPersistentCoordinateFrames failed to initialize, disabling script. Reason: {0}", status);
                enabled = false;
            }
        }

        void HandleContentStatusUpdate(MLPersistentBehavior.Status status, MLResult result)
        {
            switch (status)
            {
                case MLPersistentBehavior.Status.BINDING_CREATED:
                    _countCreatedGood++;
                    UpdateCreatedCountText();
                    break;
                case MLPersistentBehavior.Status.BINDING_CREATE_FAILED:
                    _countCreatedBad++;
                    UpdateCreatedCountText();
                    ShowError(result);
                    break;
                case MLPersistentBehavior.Status.RESTORE_SUCCESSFUL:
                    _countRestoredGood++;
                    UpdateRestoredCountText();
                    break;
                case MLPersistentBehavior.Status.RESTORE_FAILED:
                    _countRestoredBad++;
                    UpdateRestoredCountText();

                    if (result.Code != MLResultCode.SnapshotPoseNotFound)
                    {
                        ShowError(result);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion 
       
        void PerformStartup()
        {
            MLInput.OnControllerButtonDown += HandleControllerButtonDown;
            ReadAllStoredObjects();
        }

        void UpdateRestoredCountText()
        {
            _countRestoredText.text = string.Format(_countRestoredTextFormat, _countRestoredGood, _countRestoredBad);
        }

        void UpdateCreatedCountText()
        {
            _countCreatedText.text = string.Format(_countCreatedTextFormat, _countCreatedGood, _countCreatedBad);
        }

        void ShowError(MLResult result)
        {
            Debug.LogErrorFormat("Error: {0}", result);
        }

        void ReadAllStoredObjects()
        {
            List<MLContentBinding> allBindings = MLPersistentStore.AllBindings;
            foreach (MLContentBinding binding in allBindings)
            {
                GameObject gameObj = Instantiate(_content, Vector3.zero, Quaternion.identity);
                gameObj.name = binding.ObjectId;
                MLPersistentBehavior persistentBehavior = gameObj.GetComponent<MLPersistentBehavior>();
                _pointBehaviors.Add(persistentBehavior);
                AddContentListeners(persistentBehavior);
            }
        }

        void CreateContent(Vector3 position, Quaternion rotation)
        {
            GameObject gameObj = Instantiate(_content, position, rotation);
            MLPersistentBehavior persistentBehavior = gameObj.GetComponent<MLPersistentBehavior>();
            persistentBehavior.UniqueId = Guid.NewGuid().ToString();
            _pointBehaviors.Add(persistentBehavior);
            AddContentListeners(persistentBehavior);
        }

        void RemoveContent(GameObject gameObj)
        {
            MLPersistentBehavior persistentBehavior = gameObj.GetComponent<MLPersistentBehavior>();
            RemoveContentListeners(persistentBehavior);
            _pointBehaviors.Remove(persistentBehavior);
            persistentBehavior.DestroyBinding();
            //Instantiate(_destroyedContentEffect, persistentBehavior.transform.position, Quaternion.identity);

            Destroy(persistentBehavior.gameObject);
        }

        void AddContentListeners(MLPersistentBehavior persistentBehavior)
        {
            persistentBehavior.OnStatusUpdate += HandleContentStatusUpdate;

            PersistentBall contentBehavior = persistentBehavior.GetComponent<PersistentBall>();
            contentBehavior.OnContentDestroy += RemoveContent;
        }

        void RemoveContentListeners(MLPersistentBehavior persistentBehavior)
        {
            persistentBehavior.OnStatusUpdate -= HandleContentStatusUpdate;

            PersistentBall contentBehavior = persistentBehavior.GetComponent<PersistentBall>();
            contentBehavior.OnContentDestroy -= RemoveContent;
        }
      
    }
}
