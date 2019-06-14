using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 0.0f;
    private float v = 0;
    public Text myText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        transform.position += transform.forward * Time.deltaTime * speed;
        speed -= transform.forward.y * Time.deltaTime * 1.0f;
            
        // transform.Rotate(-Input.GetAxis("Vertical"), 180.0f, -Input.GetAxis("Horizontal"));
        transform.Rotate(-Input.GetAxis("Vertical"), 180.0f, 0);

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

}
