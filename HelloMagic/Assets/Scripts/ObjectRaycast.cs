using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ObjectRaycast : MonoBehaviour
{
    public GameObject myObj;
    public GameObject myCamera;
    public MLInputController myController;
    private const float _distance = 1.5f;

    private void Awake()
    {
        myCamera = GameObject.Find("Main Camera");
    }

    void Start()
    {
        MLInput.Start();
        MLInput.OnControllerButtonDown += OnPressButton;
        myController = MLInput.GetController(MLInput.Hand.Left);
    }

    void Update()
    {
        transform.position = myController.Position; //Vector3
        transform.rotation = myController.Orientation; //quaterion
    }

    void OnPressButton(byte controllerID, MLInputControllerButton button)
    {
        RaycastHit hit;
        if (Physics.Raycast(myController.Position, transform.forward, out hit)) //origin direction, out hit, maxDistance
        {
            if (button == MLInputControllerButton.Bumper)
            {
                GameObject myInstObj = Instantiate(myObj, hit.point, Quaternion.Euler(hit.normal));
            }
        }
    }

    void OnDestroy()
    {
        MLInput.Stop();
        MLInput.OnControllerButtonDown -= OnPressButton;
    }

}