using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI; 

public class DragObjectController : MonoBehaviour
{
    private const float _distance = 1.1f;
    private MLInputController _controller;
    public GameObject _myUniverse, _camera;
    private bool myFlag, _instantiated;
    public Text myTextFlag;
    public ShipInput myShip;
    public GameObject myQuad;
    public GameObject myShipPrefab;

    private void Awake()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonDown += OnButtonDown;
    }

    void Start()
    {  
        _camera = GameObject.Find("Main Camera");
        _myUniverse = GameObject.Find("Universe");
        myQuad = GameObject.Find("Quad");
        myTextFlag.text = "bool: false";
        Time.timeScale = 0;
        myFlag = false;
        _instantiated = false;
        _myUniverse.GetComponent<Collider>();

    }

    public void Update()
    {
        if (myFlag == false)
        {
            _myUniverse.GetComponent<Collider>().enabled = true;

           // if (_controller.TriggerValue > 0.2)
            //{
            _myUniverse.transform.position = _controller.Position + _camera.transform.forward * _distance;
                _myUniverse.transform.rotation = _camera.transform.rotation;
            //}
            Time.timeScale = 0;
            myQuad.SetActive(true);
        }
        else if (myFlag == true) 
        {
            _myUniverse.GetComponent<Collider>().enabled = false;

            if (_instantiated == false)
            {
                GameObject myShip = Instantiate(myShipPrefab, _controller.Position + _camera.transform.forward * _distance, Quaternion.identity);
                _instantiated = true;
            }
            myQuad.SetActive(false);
            Time.timeScale = 1;

        }


    }

    void OnButtonDown(byte controller_id, MLInputControllerButton button)
    {
        if ((button == MLInputControllerButton.HomeTap))
        {
            myFlag = !myFlag;
            myTextFlag.text = "Game: " + myFlag.ToString();
            myShip.placeUniverse();
        }
    }

    void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.Stop();
    }

}
