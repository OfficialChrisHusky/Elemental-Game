using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementalPower : MonoBehaviour
{
    public enum Element {Fire,Air,Water,Earth,None}
    
    //used to cast ray from what we are seeing
    private Camera mainCamera;
    
    [Header("Element Variable")]
    public Element currentElement = Element.None;
    private GameObject currentPrefab;

    [Header("Element Prefab")] 
    public GameObject fireBall;


// Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Player>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        selectElement();
        if (currentElement != Element.None && Input.GetMouseButtonDown(0)) {
            Instantiate(currentPrefab,ray.origin, mainCamera.transform.rotation);
        }
    }

    private void selectElement()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentElement == Element.Fire) {
                currentElement = Element.None;
                return;
            }
            currentElement = Element.Fire;
            currentPrefab = fireBall;
        }
    }
}
