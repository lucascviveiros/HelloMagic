using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

/// Class specifically to deal with input.
public class ShipInput : MonoBehaviour
{
    //[Tooltip("When true, the mouse and mousewheel are used for ship input and A/D can be used for strafing like in many arcade space sims.\n\nOtherwise, WASD/Arrows/Joystick + R/T are used for flying, representing a more traditional style space sim.")]
    //public bool useMouseInput = false;
    //[Tooltip("When using Keyboard/Joystick input, should roll be added to horizontal stick movement. This is a common trick in traditional space sims to help ships roll into turns and gives a more plane-like feeling of flight.")]
    //public bool addRoll = true;

	[Space]

    [Range(-0.5f, 0.5f)]
    public float pitch;
    [Range(-0.5f, 0.5f)]
    public float yaw;
    [Range(-0.5f, 0.5f)]
    public float roll;
    [Range(-1, 1)]
    public float strafe;
    [Range(0, 1)]
    public float throttle;

    private const float THROTTLE_SPEED = 0.5f;
    private Ship ship;

    //Magic Leap Controller
    private MLInputController _controller;
    public float sideSpeed = 5.0f;
    public bool setUniverse;

    private void Awake()
    {
        ship = GetComponent<Ship>();
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        setUniverse = false;
    }

    public void Update()
    {
            //myTrigger.text = "Y: " + _controller.Orientation.y.ToString()+ "\n" + "X: " + _controller.Orientation.x.ToString() + "\n" + "Z: " + _controller.Orientation.z.ToString();

            //Aceleracao 
            float acelera = _controller.TriggerValue; 
            Update6DOFThrottle(acelera);

            //TurnRight
            if ((_controller.TouchpadGesture.Type.ToString() == "Swipe" || _controller.TouchpadGesture.Type.ToString() == "Tap")
                   // && _controller.Touch1PosAndForce.x > 0.1 
                    && _controller.Touch1PosAndForce.z > 0 && _controller.TouchpadGesture.Direction.ToString() == "Right")
            {
                TurnRightLeft(sideSpeed);
            }

            //TurnLeft
            if ((_controller.TouchpadGesture.Type.ToString() == "Swipe" || _controller.TouchpadGesture.Type.ToString() == "Tap")
                   // &&_controller.Touch1PosAndForce.x < -0.1 
                    && _controller.Touch1PosAndForce.z > 0 && _controller.TouchpadGesture.Direction.ToString() == "Left")
            {
                TurnRightLeft(-sideSpeed);
            }

            //UP
            if (_controller.Touch1PosAndForce.y > 0.1 && _controller.Touch1PosAndForce.z > 0
                        && (_controller.Touch1PosAndForce.x > -0.3 && _controller.Touch1PosAndForce.x < 0.3)
                            && (_controller.TouchpadGesture.Type.ToString() == "ForceTapUp" || _controller.TouchpadGesture.Type.ToString() == "Tap"))// && _controller.TouchpadGesture.Direction.ToString() == "Up") 
            {
                transform.Rotate(5.625f, 0.0f, 0.0f);
            }

            //DOWN
            if (_controller.Touch1PosAndForce.y < -0.1 && _controller.Touch1PosAndForce.z > 0
                        && (_controller.Touch1PosAndForce.x > -0.3 && _controller.Touch1PosAndForce.x < 0.3)
                            && (_controller.TouchpadGesture.Type.ToString() == "ForceTapDown" || _controller.TouchpadGesture.Type.ToString() == "Tap"))// && _controller.TouchpadGesture.Direction.ToString() == "Down")
            {
                transform.Rotate(-5.625f, 0.0f, 0.0f);
            }

            // Y-axis Rotation 
            /*
            if (_controller.Orientation.y < -0.2 && (_controller.Orientation.x > -0.5 && _controller.Orientation.x < 0.3)) // _controller.TriggerValue > 0.2) 
            {//
                transform.Rotate(0.0f, 0.0f, 2.8125f);
            }


            if (_controller.Orientation.y > 0.2 && (_controller.Orientation.x > -0.5 && _controller.Orientation.x < 0.3)) //&& _controller.TriggerValue>0.2)
            {
                transform.Rotate(0.0f, 0.0f, -2.8125f);
            }*/

            // Orientacao controle
            if (_controller.Orientation.x > 0.08) //sobe
            {
                transform.Rotate(-_controller.Orientation.x * 4f, 0.0f, 0.0f);

            }

            if (_controller.Orientation.x < 0.08) //desce
            {
                transform.Rotate(-_controller.Orientation.x * 4f, 0.0f, 0.0f);
            }
        
    }

    private void TurnRightLeft(float speed)
    {
        transform.Rotate(0.0f, speed, 0.0f);
    }

    public void placeUniverse() 
    {
        setUniverse = !setUniverse; 
    }

    public bool getPlaceUniverse() 
    {
        return setUniverse;
    }

    /// When the mouse is in the center of the screen, this is the same as a centered stick.
    private void SetStickCommandsUsingMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        // Figure out most position relative to center of screen.
        // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
        pitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height* 0.5f);
        yaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        // Make sure the values don't exceed limits.
        pitch = -Mathf.Clamp(pitch, -1.0f, 1.0f);
        yaw = Mathf.Clamp(yaw, -1.0f, 1.0f);
    }

    private void SetStickCommandsUsing6DOF(float my6DOFOrientation) 
    {
        yaw = (my6DOFOrientation - (Screen.width * 0.5f)) / (Screen.width * 0.5f);
        yaw = Mathf.Clamp(yaw, -1.0f, 1.0f);
    }

    /// Uses R and F to raise and lower the throttle.
    private void UpdateKeyboardThrottle(KeyCode increaseKey, KeyCode decreaseKey)
    {
        float target = throttle;

        if (Input.GetKey(increaseKey))
            target = 1.0f;
        else if (Input.GetKey(decreaseKey))
            target = 0.0f;

        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);
    }

    private void UpdateMouseWheelThrottle()
    {
        throttle += Input.GetAxis("Mouse ScrollWheel");
        throttle = Mathf.Clamp(throttle, 0.0f, 1.0f);
    }

    private void Update6DOFThrottle(float acelera)
    {
        float target = throttle;
        throttle = Mathf.Clamp(acelera, 0.0f, 1.0f);
        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);

        //AudioSource som = GetComponent<AudioSource>();
        //som.Play();
    }
}