using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI; 

public class DragObjectController : MonoBehaviour
{
    private const float _distance = 1.0f;
    private MLInputController _controller;
    public GameObject _myObject, _camera;
    private bool myFlag;
    public Text myTextFlag; 

    private void Awake()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonDown += OnButtonDown;
    }

    // Start is called before the first frame update
    void Start()
    {  
        _camera = GameObject.Find("Main Camera");
        _myObject = GameObject.Find("Cube");
        myFlag = false;
        myTextFlag.text = "Nenhum clique do hone button - flag: false"; 
    }

    // Update is called once per frame
    public void Update()
    {
        if (_controller.TriggerValue > 0.2)
        {
            _myObject.transform.position = _controller.Position + _camera.transform.forward * _distance;
            _myObject.transform.rotation = _camera.transform.rotation;
        }
    }

    void OnButtonDown(byte controller_id, MLInputControllerButton button)
    {
        if ((button == MLInputControllerButton.HomeTap))
        {
            myFlag = !myFlag;
            myTextFlag.text = "bool: " + myFlag.ToString();
        }
    }

    void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.Stop();
    }

}
