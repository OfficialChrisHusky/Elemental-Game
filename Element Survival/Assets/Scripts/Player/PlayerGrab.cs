using System;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    
    //used to cast ray from what we are seeing
    private Camera mainCamera;
    
    //Grab Variables
    private GameObject grabbedObject;
    private Rigidbody grabbedRigidbody;
    private bool isGrabbing;
    private bool placement;
    private Material defaultPlacementMaterial;

    //Throw Variables
    public float throwForce = 10f;
    
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
        if (hitObject.tag.Equals("Grabbable") && Input.GetMouseButtonDown(0) && !isGrabbing) {
            Destroy(hitObject);
            var itemIdentifier = hitObject.GetComponent<ItemIdentifier>();
            Inventory.instance.AddItem(itemIdentifier.Item,itemIdentifier.quantity);
        }

        if (isGrabbing) {
            if (Input.GetMouseButtonDown(0)) {
                isGrabbing = false;
                if (placement)  {
                    placement = false;
                    grabbedRigidbody.gameObject.GetComponent<MeshRenderer>().material = defaultPlacementMaterial;
                }
                else {
                    grabbedRigidbody.AddForce(ray.direction * throwForce, ForceMode.Impulse);
                }
            }
            if (Input.GetMouseButtonDown(1) && !placement) {
                
                isGrabbing = false;
                grabbedRigidbody = null;
            }
        }
        else {
            if (hitObject.tag.Equals("Grabbable") && Input.GetMouseButtonDown(1)) {
                grabbedRigidbody = hitObject.GetComponent<Rigidbody>();
                isGrabbing = true;
            }
        }
    }

    public void grabPlacement(GameObject obj, Material defaultMaterial)
    {
        grabbedRigidbody = obj.GetComponent<Rigidbody>();
        defaultPlacementMaterial = defaultMaterial;
        isGrabbing = true;
        placement = true;
    }

    private void FixedUpdate()
    {
        if (isGrabbing) {
            grabbedRigidbody.velocity = Vector3.zero;
            grabbedRigidbody.position =  mainCamera.transform.position + mainCamera.transform.forward * 2;
        }
    }
}
