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
    private List<Transform> _pinkyFinger;

    private MLHand Hand;
    private void Awake()
    {
        //Initiate Control
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
       // MLInput.OnControllerButtonDown += OnButtonDown;

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

        mySpherePoints = new GameObject[15];
        pos = new Vector3[15];

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
        //Array.ForEach(mySpherePoints, (GameObject obj) => Destroy(obj));
        Array.ForEach(mySpherePoints, (GameObject obj) => obj.SetActive(false));

        /*
        foreach (GameObject obj in mySpherePoints)
        {
            obj.SetActive(false);
        }*/
    }

    private void EnablePoints() 
    {
        foreach (GameObject obj in mySpherePoints)
        {
            obj.SetActive(true);
        }
    }

    private void ShowPoints()
    {
        //Pinky finger 
        pos[0] = MLHands.Left.Pinky.Tip.Position;
        pos[1] = MLHands.Left.Pinky.MCP.Position;

        //Ring finger 
        pos[2] = MLHands.Left.Ring.Tip.Position;
        pos[3] = MLHands.Left.Ring.MCP.Position;

        //Middle finger
        pos[4] = MLHands.Left.Middle.Tip.Position;
        pos[5] = MLHands.Left.Middle.MCP.Position;
        pos[6] = MLHands.Left.Middle.DIP.Position;

        //Index finger
        pos[7] = MLHands.Left.Index.Tip.Position;
        pos[8] = MLHands.Left.Index.MCP.Position;
        pos[9] = MLHands.Left.Index.DIP.Position;

        //Thumb finger
        pos[10] = MLHands.Left.Thumb.Tip.Position;
        pos[11] = MLHands.Left.Thumb.MCP.Position;
        pos[12] = MLHands.Left.Thumb.CMC.Position;

        pos[13] = MLHands.Left.Wrist.Ulnar.Position;
        pos[14] = MLHands.Left.Center;

        /*
        pos[0] = MLHands.Left.Thumb.KeyPoints[0].Position;
        pos[1] = MLHands.Left.Index.KeyPoints[0].Position;
        pos[2] = MLHands.Left.Wrist.KeyPoints[0].Position;
        */
        for (int k = 0; k < mySpherePoints.Length; k++) 
        {   
            mySpherePoints[k].transform.position = pos[k];
        }

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
    }

    public void Update()
    {
   
        if (myFlag == false)
        {
            _myUniverse.GetComponent<Collider>().enabled = true;
            Time.timeScale = 0;
            myQuad.SetActive(true);
        }

        if (myFlag == true) 
        {
            _myUniverse.GetComponent<Collider>().enabled = false;
            myQuad.SetActive(false);
            Time.timeScale = 1;
        }

        //Open hand back gesture for moving the Universe
        if (GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack))
        {
            _myUniverse.transform.position = _controller.Position + _camera.transform.forward * _distance;
            _myUniverse.transform.rotation = _camera.transform.rotation;
            myHands.text = "OpenHandBack recognized";
            myFlag = false;
            EnablePoints();
            ShowPoints();
        }

        //Positive finger gesture for place the Universe
        if (GetGesture(MLHands.Left, MLHandKeyPose.Thumb))
        {
            _myUniverse.GetComponent<Collider>().enabled = false;
            myHands.text = "Thumb Gesture Recognized";
            myFlag = true;
            EnablePoints();
            ShowPoints();

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

        if (GetGesture(MLHands.Left,MLHandKeyPose.NoHand)) 
        {
            DisablePoints();
        }
        //if (pose != HandPoses.NoPose) ShowPoints();

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
        MLHands.Stop();
    }

}
