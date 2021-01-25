using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform target;
    public float speed = 10.0f;
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
        float y = Mathf.Sin(dir.normalized.y);
        float z = dir.normalized.z;
        transform.Translate(new Vector3(x,y,z) * DistaceTime, Space.World);
        
    }

    public void HitTarget()
    {
        //Particle
        Destroy(gameObject);
        Debug.Log("Error");
    }
}
