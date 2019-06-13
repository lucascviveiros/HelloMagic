using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

public class MyControl : MonoBehaviour
{
    public MLPersistentBehavior persistentBehavior;
    private GameObject _monalisa, _camera;
    private MLInputController _controller;
    private const float _distance = 2.0f;

    private void Awake()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        _camera = GameObject.Find("Main Camera");
        _monalisa = GameObject.Find("Monalisa");
    }

    private void OnDestroy()
    {
        MLInput.Stop();
    }

    private void Update()
    {
        if (_controller.TriggerValue > 0.2f)
        {
            _monalisa.transform.position = _controller.Position + _camera.transform.forward * _distance;
            _monalisa.transform.rotation = _camera.transform.rotation;
            persistentBehavior.UpdateBinding();
        }
    }

}
