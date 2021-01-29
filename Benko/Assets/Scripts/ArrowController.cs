using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowController : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector3 start;
    float damage;
    public GameObject PopUpText;
    private TextMeshPro TextMesh;
    void Start() {
        start = transform.position;
        setStartDir();
        //TextMesh = GetComponent<TextMeshPro>();
    }
    public void Seek(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;
        if(target==null){
            Destroy(gameObject);
        }
        
    }
    void Update()
    {
        Vector3 prevPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
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
            var targetRotation = Quaternion.LookRotation(face);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping); 
        }
        // Vector3 dir = target.position - transform.position;
        // Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Vector3 rotation = lookRotation.eulerAngles;
        // transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void setStartDir() {
        if(target==null) return;
        Vector3 v = target.position - start;
        v.y = 0;
        v = v.normalized;
        v.y = 2;
        transform.rotation = Quaternion.LookRotation(v);
    }
    public void HitTarget()
    {
        //Particle Hit
        target.gameObject.GetComponent<Health>().Currenthealth -= damage;
        GameObject go = Instantiate(PopUpText, target.transform.position + new Vector3(0,2,0), Quaternion.identity);
        TextMesh = go.GetComponent<TextMeshPro>();
        TextMesh.SetText(damage.ToString());
        Destroy(go,0.9f);
        Destroy(gameObject);

        GameObject _camera = GameObject.Find("_camera");
        transform.rotation = _camera.transform.rotation;
}
}
