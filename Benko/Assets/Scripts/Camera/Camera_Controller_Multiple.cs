using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera_Controller_Multiple : MonoBehaviour
{
    public List<Transform> targets;
    public GameObject[] targetss;

    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime = 0.5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomlimiter = 50f;
    public Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        targetss = GameObject.FindGameObjectsWithTag("Unit");
        //List<GameObject> targetsss 
        //var sceneObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Clone"));

        //targets = sceneObjects;

        if (targetss.Length == 0)
        {
            return;
        }
        Move();
        Zoom();


    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomlimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targetss[0].transform.position, Vector3.zero);
        for (int i = 0; i < targetss.Length; i++)
        {
            bounds.Encapsulate(targetss[i].transform.position);
        }

        return Mathf.Max(bounds.size.x, bounds.size.y);
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if(targetss.Length == 1)
        {
            return targetss[0].transform.position;
        }

        var bounds = new Bounds(targetss[0].transform.position, Vector3.zero);
        for (int i = 0; i < targetss.Length; i++)
        {
            bounds.Encapsulate(targetss[i].transform.position);
        }

        return bounds.center;
    }

}
