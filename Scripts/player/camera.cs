using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {   Vector3 newpos =  new Vector3 (transform.position.x, transform.position.y,  offset.z+target.position.z);
        transform.position =  Vector3.Lerp(transform.position,newpos,500*Time.deltaTime);
        
    }
}
