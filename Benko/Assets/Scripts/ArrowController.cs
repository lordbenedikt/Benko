using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform target;
    public float speed;
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
        Vector3 prevPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);

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

        float origTargetDistance = new Vector2(start.x-target.position.x, start.z-target.position.z).magnitude;
        float travelledDistance = new Vector2(start.x-transform.position.x, start.z-transform.position.z).magnitude / new Vector2(start.x-target.position.x, start.z-target.position.z).magnitude;
        if(travelledDistance > 1) {
            Destroy(gameObject);
            return;
        }
        float arrowHeight = origTargetDistance*0.3f;
        
        transform.position = new Vector3(transform.position.x, -Mathf.Pow((travelledDistance*2-1),2)*arrowHeight + (arrowHeight) + target.position.y, transform.position.z);
        Vector3 face = new Vector3(transform.position.x-prevPos.x,transform.position.y-prevPos.y,transform.position.z-prevPos.z);
        
        if(face.sqrMagnitude != 0) {
            float damping = 50f;
    
            // face.y = 0;
            var targetRotation = Quaternion.LookRotation(face);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping); 
        }

        // Vector3 dir = target.position - transform.position;
        // Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Vector3 rotation = lookRotation.eulerAngles;
        // transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    public void HitTarget()
    {
        //Particle
        Destroy(gameObject);
        // Debug.Log("Error");
    }
}
