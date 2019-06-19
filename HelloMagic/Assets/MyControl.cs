using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

public class MyControl : MonoBehaviour
{
    public MLPersistentBehavior persistentBehavior;
    private GameObject _myObject, _camera;
    private MLInputController _controller;
    private const float _distance = 1.0f;

    public Text myText;

    public SpaceshipController spaceship;

    private void Awake()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        _camera = GameObject.Find("Main Camera");
        _myObject = GameObject.Find("PlayerShip");
    }

    private void OnDestroy()
    {
        MLInput.Stop();
    }

    private void Update()
    {
        //if (_controller.TriggerValue > 0.2f)
       // {
       //     _myObject.transform.position = _controller.Position + _camera.transform.forward * _distance;
       //     _myObject.transform.rotation = _camera.transform.rotation;
      //      persistentBehavior.UpdateBinding();
      //  }

        if (_controller.IsBumperDown) {
             _myObject.transform.position = _controller.Position + _camera.transform.forward * _distance;
             _myObject.transform.rotation = _camera.transform.rotation;
            // persistentBehavior.UpdateBinding();

        }
      
    }

}
