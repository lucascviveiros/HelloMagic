using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class TouchpadGestures : MonoBehaviour
{
    public Text typeText, stateText, directionText;
    public Camera Camera;
    public SpaceshipController spaceship;

    #region Private Variables
    private MLInputController _controller;

    #endregion
    
    #region Unity Methods
    void Start()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        Time.timeScale = 1.0f;
    }

    void OnDestroy()
    {
        MLInput.Stop();
    }

    void Update()
    {
        updateTransform();
        updateGestureText();
    }

    #endregion

    #region Private Methods
    void updateGestureText()
    {
        string gestureType = _controller.TouchpadGesture.Type.ToString();
        string gestureState = _controller.TouchpadGestureState.ToString();
        string gestureDirection = _controller.TouchpadGesture.Direction.ToString();

        typeText.text = "Type: " + gestureType;
        stateText.text = "State: " + gestureState;
        directionText.text = "Direction: " + gestureDirection;

        if (gestureDirection == "up" || gestureDirection == "Up" || gestureType == "Tap") 
        {
        } 

        if(gestureDirection == "down") 
        {
            spaceship.BackSpaceship();
        }
    }

    //state: start, continue, end
    //direction: up, down, left, right

    void updateTransform()
    {
        float speed = Time.deltaTime * 5f;

        Vector3 pos = Camera.transform.position + Camera.transform.forward;
        gameObject.transform.position = Vector3.SlerpUnclamped(gameObject.transform.position, pos, speed);

        Quaternion rot = Quaternion.LookRotation(gameObject.transform.position - Camera.transform.position);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rot, speed);
    }
    #endregion
}