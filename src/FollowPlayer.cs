using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.0f, 3.0f, 2.0f);
    private Transform target;
    public float sensibility = 5.0f;
    [Range (0, 1)] public float lerpValue = 0.1f;
    public float correction = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("PlayerCameraRoot").transform;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibility, Vector3.up) * offset;   
        transform.LookAt(target);
        transform.Translate(0,correction,0);
    }

}