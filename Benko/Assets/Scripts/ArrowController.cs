using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform target;
    public float speed = 10.0f;
    public Vector3 start;

    
    void Start() {
        start = transform.position;
    }
    public void Seek(Transform _target)

    {
        target = _target;
    }
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            
            return;
        }
        //Debug.Log(target);

        Vector3 dir = target.position - transform.position; 
        float DistaceTime = speed * Time.deltaTime;

        if(dir.magnitude <= DistaceTime)
        {
            HitTarget();
            return;
        }
        float x = dir.normalized.x;
        float z = dir.normalized.z;
        
        transform.Translate(new Vector3(x, 0, z) * DistaceTime, Space.World);

        float travelledDistance = new Vector2(start.x-transform.position.x, start.z-transform.position.z).magnitude / new Vector2(start.x-target.position.x, start.z-target.position.z).magnitude;
        if(travelledDistance > 1) {
            Destroy(gameObject);
            return;
        }

        transform.position = new Vector3(transform.position.x, Mathf.Sin((travelledDistance)*Mathf.PI)*5 + target.position.y, transform.position.z);
        
    }

    public void HitTarget()
    {
        //Particle
        Destroy(gameObject);
        // Debug.Log("Error");
    }
}
