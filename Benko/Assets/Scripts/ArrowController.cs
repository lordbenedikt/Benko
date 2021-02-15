using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowController : MonoBehaviour
{
    public GameObject target;
    public float speed;
    [HideInInspector]
    public Vector3 start;
    float damage;
    public GameObject PopUpText;
    private TextMeshPro TextMesh;
    public GameObject HitFx;
    private Animator anim;
    void Start() {
        start = transform.position;
        SetStartDir();
    }
    public void Seek(GameObject _target, float _damage)
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
        Vector3 dir = target.transform.position - transform.position;
        float DistaceTime = speed * Time.deltaTime;
        if(dir.magnitude <= DistaceTime)
        {
            HitTarget();
            return;
        }
        float x = dir.normalized.x;
        float z = dir.normalized.z;
        transform.Translate(new Vector3(x, 0, z) * DistaceTime, Space.World);
        float origTargetDistance = new Vector2(start.x-target.transform.position.x, start.z-target.transform.position.z).magnitude;
        float travelledDistance = new Vector2(start.x-transform.position.x, start.z-transform.position.z).magnitude / new Vector2(start.x-target.transform.position.x, start.z-target.transform.position.z).magnitude;
        if(travelledDistance > 1) {
            Destroy(gameObject);
            return;
        }
        float arrowHeight = origTargetDistance*0.3f;
        transform.position = new Vector3(transform.position.x, -Mathf.Pow((travelledDistance*2-1),2)*arrowHeight + (arrowHeight) + target.transform.position.y, transform.position.z);
        Vector3 face = new Vector3(transform.position.x-prevPos.x,transform.position.y-prevPos.y,transform.position.z-prevPos.z);
        if(face.sqrMagnitude != 0) {
            //float damping = 50f;
            transform.rotation = Quaternion.LookRotation(face);
            transform.Rotate(new Vector3(0,-90,0));
            // transform.rotation = Quaternion.Euler(transform.rotation.x+90,transform.rotation.y+90,transform.rotation.z);
            // var targetRotation = Quaternion.LookRotation(face);
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
        }

    }
    void SetStartDir() {
        if(target==null) return;
        Vector3 v = target.transform.position - start;
        v.y = 0;
        v = v.normalized;
        v.y = 2;
        transform.rotation = Quaternion.LookRotation(v);
        // transform.rotation = Quaternion.Euler(0,0,90);
    }
    public void HitTarget()
    {
        if (target == null) Destroy(gameObject);
        GameObject fx = Instantiate(HitFx, target.transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(fx,2f);
        if (target == null) Destroy(gameObject);
        target.gameObject.GetComponent<Health>().Currenthealth -= damage;

        GameObject go = Instantiate(PopUpText, target.transform.position + new Vector3(0,1.3f,0), Quaternion.identity);
        anim = go.GetComponent<Animator>();
        anim.SetBool("start", true);

        TextMesh = go.GetComponent<TextMeshPro>();
        TextMesh.SetText("-" + damage.ToString());
        Destroy(go,0.8f);
        Destroy(gameObject);

        GameObject _camera = GameObject.Find("_camera");
        transform.rotation = _camera.transform.rotation;
}
}
