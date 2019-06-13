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
using UnityEngine.XR.MagicLeap;

namespace MagicLeap
{
    /// This class demonstrates using the MLMediaPlayer API
    public class MediaPlayerExample : MonoBehaviour
    {
        #region Private Variables

        [SerializeField, Tooltip("MeshRenderer to display media")]
        private MeshRenderer _screen = null;

        [SerializeField, Tooltip("URL of Video to be played")]
        private string _url = String.Empty;

        // Private class used to facilitate "Dictionary" inspector, since Unity can't inspect Dictionaries
        [System.Serializable]
        private class StringKeyValue
        {
            public string Key = "";
            public string Value = "";
        }

        private MLMediaPlayer _mediaPlayer = null;
        private bool _isSeeking = false;
        private bool _isBuffering = false;
        private float _UIUpdateTimer;
        #endregion // Private Variables

        #region Unity Methods
        private void Awake()
        {

            _mediaPlayer = _screen.gameObject.AddComponent<MLMediaPlayer>();
        }

        private void Start()
        {
            _mediaPlayer.VideoSource = _url;

            MLResult result = _mediaPlayer.PrepareVideo();
            if (!result.IsOk)
            {
                if (result.Code == MLResultCode.PrivilegeDenied)
                {
                    Instantiate(Resources.Load("PrivilegeDeniedError"));
                }
            }
        }

        #endregion // Unity Methods

        #region Private Methods
        /// Function to update the elapsed time text
        private void UpdateElapsedTime(long elapsedTimeMs)
        {
            TimeSpan timeSpan = new TimeSpan(elapsedTimeMs * TimeSpan.TicksPerMillisecond);
        }

    
        #endregion // Private Methods

        #region Event Handlers

        /// Handler when Play/Pause Toggle is triggered.
        private void PlayPause(bool shouldPlay)
        {
            if (_mediaPlayer != null)
            {
                if (!shouldPlay && _mediaPlayer.IsPlaying)
                {
                    _UIUpdateTimer = float.MaxValue;
                    _mediaPlayer.Pause();
                }
                else if (shouldPlay && !_mediaPlayer.IsPlaying)
                {
                    _UIUpdateTimer = float.MaxValue;
                    _mediaPlayer.Play();
                }
            }
        }

        /// Handler when Stop button has been triggered. See HandleStop() for more info.
        private void Stop()
        {
            _UIUpdateTimer = float.MaxValue;
            _mediaPlayer.Stop();
        }


        /// Handler when Timeline Slider has changed value. Moves the play head to a specific percentage of the whole duration.
        private void Seek(float sliderValue)
        {
            if (Mathf.Approximately(sliderValue, _mediaPlayer.AnimationPosition))
            {
                return;
            }

            _mediaPlayer.Seek(sliderValue);
        }

        /// Handler when Volume Sider has changed value.
        private void SetVolume(float sliderValue)
        {
            _mediaPlayer.SetVolume(sliderValue);
        }
        #endregion // Event Handlers
    }
}

