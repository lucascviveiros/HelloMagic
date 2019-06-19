using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 0.0f;
    private float v = 0;
    public Text myText;

    [Range(0, 1)]
    public float throttle;

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    private MLInputController _controller;


    private void Awake()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    void FixedUpdate()
    {
        //transform.Rotate(new Vector3(0, 180, 0));
        //transform.position += transform.forward * Time.deltaTime * speed;
       // speed -= transform.forward.y * Time.deltaTime * 1.0f;
            
        // transform.Rotate(-Input.GetAxis("Vertical"), 180.0f, -Input.GetAxis("Horizontal"));
        //transform.Rotate(-Input.GetAxis("Vertical"), 180.0f, 0);


        //Aceleracao 
        float acelera = _controller.TriggerValue; //0 a 1
        Update6DOFThrottle(acelera);
    }

    public void GoSpaceship(float v) 
    {
        speed = v;
        myText.text = "GOOO";
    }

    public void BackSpaceship() 
    {
        speed--;
    }

    public void TurnUp() 
    {
        transform.Rotate(-Input.GetAxis("Vertical"), 180.0f, 0);
    }

    public void TurnDown() 
    {
        transform.Rotate(-Input.GetAxis("Vertical"), 180.0f, 0);
    }

    public void TurnRight() 
    {
        transform.Rotate(0, 0, +30);
    }

    public void TurnLeft() 
    {
        transform.Rotate(0,0,0);
    }



    private void Update6DOFThrottle(float acelera)
    {
        float target = throttle;
        throttle = Mathf.Clamp(acelera, 0.0f, 1.0f);
        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);

    }

}
