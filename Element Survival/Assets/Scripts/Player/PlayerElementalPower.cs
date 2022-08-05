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

    [SerializeField]
    private float increment = 1f;

// Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Player>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Input.GetKeyDown(KeyCode.F)) {
            selectElement(Element.Fire);
        }

        if (currentElement != Element.None)
        {
            if (Input.GetMouseButton(0))
            {
                if(increment < 1.25f)
                    increment += 0.001f;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                var obj = Instantiate(currentPrefab,ray.origin, mainCamera.transform.rotation);
                obj.GetComponentInChildren<Fire>().multiplyDamage(increment);
                increment = 1f;
            }

        }
    }

    public void selectElement(Element toSelect)
    {
        switch (toSelect)
        {
            case Element.Fire when currentElement == Element.Fire:
                currentElement = Element.None;
                return;
            case Element.Fire:
                currentElement = Element.Fire;
                currentPrefab = fireBall;
                break;
        }
    }
}
