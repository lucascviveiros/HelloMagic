using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PlaneRecognition : MonoBehaviour
{

    public Transform BBoxTransform;  // used to determine the center of the region in which the plane extraction happens. Use the head pose through the MainCamera
    public Vector3 BBoxExtents;  //determines the size of the region. If the bounding box extent is 0,0,0, the size of the region from which you can extract planes is boundless.
    public GameObject PlaneGameObject;

    public MLWorldPlanesQueryFlags QueryFlags;

    private float timeout = 5f;
    private float timeSinceLastRequest = 0f;

    private MLWorldPlanesQueryParams _queryParams = new MLWorldPlanesQueryParams();
    private List<GameObject> _planeCache = new List<GameObject>();  //List of planes saved

    // Use this for initialization
    void Start()
    {
        MLWorldPlanes.Start(); //start the planes recognition
    }

    private void OnDestroy()
    {
        MLWorldPlanes.Stop();    
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastRequest += Time.deltaTime;
        if (timeSinceLastRequest > timeout)
        {
            timeSinceLastRequest = 0f;
            RequestPlanes();
        }
    }

    void RequestPlanes()
    {
        _queryParams.Flags = QueryFlags;
        _queryParams.MaxResults = 100;
        _queryParams.BoundsCenter = BBoxTransform.position;
        _queryParams.BoundsRotation = BBoxTransform.rotation;
        _queryParams.BoundsExtents = BBoxExtents;

        MLWorldPlanes.GetPlanes(_queryParams, HandleOnReceivedPlanes);
    }

    private void HandleOnReceivedPlanes(MLResult result, MLWorldPlane[] planes, MLWorldPlaneBoundaries[] boundaries)
    {

        for (int i = _planeCache.Count - 1; i >= 0; --i)
        {
            Destroy(_planeCache[i]);
            _planeCache.Remove(_planeCache[i]);
        }

        GameObject newPlane;
        for (int i = 0; i < planes.Length; ++i)
        {
            newPlane = Instantiate(PlaneGameObject);
            newPlane.transform.position = planes[i].Center;
            newPlane.transform.rotation = planes[i].Rotation;
            newPlane.transform.localScale = new Vector3(planes[i].Width, planes[i].Height, 1f);
            _planeCache.Add(newPlane);
        }
    }

}
