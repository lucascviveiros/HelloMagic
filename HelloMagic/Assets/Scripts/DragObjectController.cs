using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using System;

public class DragObjectController : MonoBehaviour
{
    private const float _distance = 1.1f;
    private MLInputController _controller;
    public GameObject _myUniverse, _camera;
    private bool myFlag, _instantiated;
    public Text myTextFlag, myHands, myFinger;
    public ShipInput myShip;
    public GameObject myQuad;
    public GameObject myShipPrefab;
    private MLHandKeyPose[] gestures;
    public PlanetsController[] planetsController;
    public Vector3[] pos;
    public GameObject mySingleSphere;
    public GameObject[] mySpherePoints;
    public enum HandPoses { Ok, Finger, Thumb, OpenHandBack, Fist, NoPose };
    public HandPoses pose = HandPoses.NoPose;
    private MLHand Hand;
    private Color _myCubecolor = Color.red;
    public GameObject _myCube, _myEye;
    private Vector3 headlook;               // where you're looking

    private void Awake()
    {
        //Initiate Control
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        // MLInput.OnControllerButtonDown += OnButtonDown;

        //Initate Eyes
        MLEyes.Start();

        //Initiate Hands
        MLHands.Start();
        //Initiate Array Gestures
        gestures = new MLHandKeyPose[10];
        gestures[0] = MLHandKeyPose.Fist; //Close hand gesture
        gestures[1] = MLHandKeyPose.Thumb; //Positive gesture
        gestures[2] = MLHandKeyPose.C; //"C" gesture
        gestures[3] = MLHandKeyPose.Finger; //Forefinger gesture
        gestures[4] = MLHandKeyPose.OpenHandBack; //Open hand back gesture
        gestures[5] = MLHandKeyPose.L; //"L" gesture
        gestures[6] = MLHandKeyPose.Pinch; //pinch gesture
        gestures[7] = MLHandKeyPose.Ok; //"ok" gesture
        gestures[8] = MLHandKeyPose.NoPose; //hand detected but no pose
        gestures[9] = MLHandKeyPose.NoHand; //No hand detected

        MLHands.KeyPoseManager.EnableKeyPoses(gestures, true, false);

        mySpherePoints = new GameObject[16];
        pos = new Vector3[16];

    }

    void Start()
    {
        _camera = GameObject.Find("Main Camera");
        _myUniverse = GameObject.Find("Universe");
        myQuad = GameObject.Find("Quad");
        myTextFlag.text = "1 - Use your left hand to change the Universe position \n2 - Positive gesture for placing the Universe";
        Time.timeScale = 0;
        myFlag = false;
        _instantiated = false;
        _myUniverse.GetComponent<Collider>();
        InitiatePoints();

        _myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _myCube.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        _myCube.GetComponent<Renderer>().material.color = _myCubecolor;
        _myCube.GetComponent<Collider>();
        _myCube.name = "CUBO";

        _myEye = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _myEye.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        _myEye.GetComponent<Renderer>().material.color = Color.green;
        _myEye.GetComponent<Collider>();

    }


    private void InitiatePoints() 
    {
        for (int k = 0; k < mySpherePoints.Length; k++)
        {
            mySpherePoints[k] = Instantiate(mySingleSphere);
            mySpherePoints[k].name = "point" + k;
        }

    }

    private void DisablePoints() 
    {
        Array.ForEach(mySpherePoints, (GameObject obj) => obj.SetActive(false));
    }

    private void EnablePoints() 
    {
        foreach (GameObject obj in mySpherePoints)
        {
            obj.SetActive(true);
        }
    }

    private void TrackPoints()
    {
        //Pinky finger 
        pos[0] = MLHands.Left.Pinky.Tip.Position;
        pos[1] = MLHands.Left.Pinky.MCP.Position;
        pos[2] = MLHands.Left.Pinky.PIP.Position;

        //Ring finger 
        pos[3] = MLHands.Left.Ring.Tip.Position;
        pos[4] = MLHands.Left.Ring.MCP.Position;
        pos[5] = MLHands.Left.Ring.PIP.Position;

        //Middle finger
        pos[6] = MLHands.Left.Middle.Tip.Position;
        pos[7] = MLHands.Left.Middle.MCP.Position;
        pos[8] = MLHands.Left.Middle.PIP.Position;

        //Index finger
        pos[9] = MLHands.Left.Index.Tip.Position;
        pos[10] = MLHands.Left.Index.MCP.Position;
        pos[11] = MLHands.Left.Index.PIP.Position;

        //Thumb finger
        pos[12] = MLHands.Left.Thumb.Tip.Position;
        pos[13] = MLHands.Left.Thumb.MCP.Position;
        pos[14] = MLHands.Left.Thumb.CMC.Position;

        pos[15] = MLHands.Left.Center;

        for (int k = 0; k < mySpherePoints.Length; k++) 
        {   
            mySpherePoints[k].transform.position = pos[k];
        }

    }

    public void Update()
    {
        Time.timeScale = 1;
        TrackPoints();
        EnablePoints();

        if (MLEyes.IsStarted)
        {
            headlook = MLEyes.FixationPoint - _camera.transform.position;

            RaycastHit _hit;
            if (Physics.Raycast(_camera.transform.position, headlook, out _hit))
            {
                _myEye.transform.position = _hit.point;
                _myEye.transform.LookAt(_hit.normal + _hit.point);

                if(_hit.collider.name == "CUBO") 
                { 
                    _myCube.transform.Rotate(Vector3.up, 3 * Time.deltaTime);
                    _myCube.GetComponent<Renderer>().material.color = Color.blue;
                    myFinger.text = "Colidiu!";
                }
            }
        }


        if (myFlag) 
        {
          _myUniverse.GetComponent<Collider>().enabled = false;
          myQuad.SetActive(false);
          Time.timeScale = 1;
        }
        else
        {
            _myUniverse.GetComponent<Collider>().enabled = true;
            //Time.timeScale = 0;
            myQuad.SetActive(true);
        }
       
        //Open hand back gesture for moving the Universe
        if (GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack))
        {
            _myUniverse.transform.position = _controller.Position + _camera.transform.forward * _distance;
            _myUniverse.transform.rotation = _camera.transform.rotation;
            myHands.text = "OpenHandBack recognized";
            myFlag = false;
            pose = HandPoses.OpenHandBack;
            _myCube.transform.position = new Vector3(mySpherePoints[15].transform.position.x, mySpherePoints[15].transform.position.y + 0.1f, mySpherePoints[15].transform.position.z);
            _myCube.transform.rotation = mySpherePoints[15].transform.rotation; 

        }
        //Positive finger gesture for place the Universe
        else if (GetGesture(MLHands.Left, MLHandKeyPose.Thumb))
        {
            _myUniverse.GetComponent<Collider>().enabled = false;
            myHands.text = "Thumb Gesture Recognized";
            myFlag = true;
            pose = HandPoses.Thumb;

            if (_instantiated == false)
            {
                GameObject myShip = Instantiate(myShipPrefab, _controller.Position + _camera.transform.forward * _distance, Quaternion.identity);
                _instantiated = true;
            }

            for (int i = 0; i < planetsController.Length; i++)
            {
                planetsController[i].setSphereCollider();
            }
        }

        else if (pose == HandPoses.NoPose || GetGesture(MLHands.Left, MLHandKeyPose.NoHand))
        {
            DisablePoints();
        }
    }


    void OnButtonDown(byte controller_id, MLInputControllerButton button)
    {
        if ((button == MLInputControllerButton.HomeTap))
        {
            myFlag = !myFlag;
            myTextFlag.text = "";
            myShip.placeUniverse();
        }
    }

    bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnDestroy()
    {
       // MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.Stop();
        if (MLHands.IsStarted)
        {
            MLHands.Stop();
        }
    }

}
