using System;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    
    //used to cast ray from what we are seeing
    private Camera mainCamera;
    
    private GameObject grabbedObject;
    private Rigidbody grabbedRigidbody;
    private bool isGrabbing;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Player>().cam;
        isGrabbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //create a ray starting from the center of the viewport (what the camera see)
        var ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        Debug.DrawRay(ray.origin,ray.direction);
        
        if (!Physics.Raycast(ray, out var hitInfo)) return;
        
        var hitObject = hitInfo.collider.gameObject;
        if (hitObject.tag.Equals("Grabbable") && Input.GetMouseButtonDown(0))
            Destroy(hitObject);

        if (!Input.GetMouseButtonDown(1)) return;
        switch (isGrabbing)
        {
            case true:
                isGrabbing = false;
                grabbedRigidbody = null;
                break;
            case false when hitObject.tag.Equals("Grabbable") && Input.GetMouseButtonDown(1):
                grabbedRigidbody = hitObject.GetComponent<Rigidbody>();
                isGrabbing = true;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (isGrabbing)
        {
            grabbedRigidbody.velocity = Vector3.zero;
            grabbedRigidbody.position =  mainCamera.transform.position + mainCamera.transform.forward * 2;
        }
    }
}
