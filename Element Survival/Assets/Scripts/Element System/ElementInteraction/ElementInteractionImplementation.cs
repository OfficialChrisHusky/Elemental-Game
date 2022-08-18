using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementInteractionImplementation : MonoBehaviour
{
    public abstract void AirInteraction();
    public abstract void EarthInteraction();
    public abstract void FireInteraction();
    public abstract void WaterInteraction();

    public void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Fire":
                FireInteraction();
                break;
            case "Earth":
                EarthInteraction();
                break;
            case "Air":
                AirInteraction();
                break;
            case "Water":
                WaterInteraction();
                break;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
