using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsController : MonoBehaviour
{
    public bool setCollider;
    public SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        setCollider = false;
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (setCollider) 
        {
            sphereCollider.GetComponent<SphereCollider>().enabled = true;
        }
    }

    public void setSphereCollider() 
    { 
        if (!setCollider) 
        {
            setCollider = true;
        }
    }

    public bool getStatus() 
    {
        return setCollider;
    }
}
