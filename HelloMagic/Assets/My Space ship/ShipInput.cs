using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

/// Class specifically to deal with input.
public class ShipInput : MonoBehaviour
{
    [Tooltip("When true, the mouse and mousewheel are used for ship input and A/D can be used for strafing like in many arcade space sims.\n\nOtherwise, WASD/Arrows/Joystick + R/T are used for flying, representing a more traditional style space sim.")]
    public bool useMouseInput = true;
    [Tooltip("When using Keyboard/Joystick input, should roll be added to horizontal stick movement. This is a common trick in traditional space sims to help ships roll into turns and gives a more plane-like feeling of flight.")]
    public bool addRoll = true;

	[Space]

    [Range(-1, 1)]
    public float pitch;
    [Range(-1, 1)]
    public float yaw;
    [Range(-1, 1)]
    public float roll;
    [Range(-1, 1)]
    public float strafe;
    [Range(0, 1)]
    public float throttle;

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    // Keep a reference to the ship this is attached to just in case.
    private Ship ship;

    //Magic Leap Controller
    private MLInputController _controller;
    public Text myOrientation;
    public Text myTrigger;
    bool flag = false;


    private void Awake()
    {
        ship = GetComponent<Ship>();
        //Control Set up
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
    }

    private void FixedUpdate()
    {
        if (useMouseInput)
        {
            strafe = Input.GetAxis("Horizontal");
            SetStickCommandsUsingMouse();
            UpdateMouseWheelThrottle();
            UpdateKeyboardThrottle(KeyCode.W, KeyCode.S);
        }

        if (_controller.Connected)
        {
            myOrientation.text = "" + _controller.Orientation;
            myTrigger.text = "pitch: " + _controller.TouchpadGesture.Direction.ToString();

            float acelera = _controller.TriggerValue; //0 a 1
            Update6DOFThrottle(acelera);

            //pitch = _controller.Orientation.
            //Vector3 contro = _controller.Orientation.z;

            // pitch = _controller.TouchpadGesture.Speed;


            //            yaw = _controller.Orientation.z; //rotacao esq direita

        
            pitch = _controller.Orientation.y;
            yaw = _controller.Orientation.z;

        }



        else
        {                
           pitch = Input.GetAxis("Vertical");
           yaw = Input.GetAxis("Horizontal");

           if (addRoll)
               roll = -Input.GetAxis("Horizontal") * 0.5f;

           strafe = 0.0f;
           UpdateKeyboardThrottle(KeyCode.R, KeyCode.F);
        }

    }

    /// Freelancer style mouse controls. This uses the mouse to simulate a virtual joystick.
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

    /// Uses the mouse wheel to control the throttle.
    private void UpdateMouseWheelThrottle()
    {
        throttle += Input.GetAxis("Mouse ScrollWheel");
        throttle = Mathf.Clamp(throttle, 0.0f, 1.0f);

    }

    private void Update6DOFThrottle(float acelera)
    {
        float target = throttle;

      //  if (acelera < 0.8) 
      //  { 
            throttle = Mathf.Clamp(acelera, 0.0f, 1.0f);
            throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);

      //  }
    }
}