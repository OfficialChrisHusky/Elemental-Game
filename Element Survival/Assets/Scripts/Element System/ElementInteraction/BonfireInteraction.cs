using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireInteraction : ElementInteractionImplementation
{
    public GameObject fireEffect;

    public override void AirInteraction()
    {
        throw new System.NotImplementedException();
    }

    public override void EarthInteraction()
    {
        throw new System.NotImplementedException();
    }

    public override void FireInteraction()
    {
        fireEffect.SetActive(true);
    }

    public override void WaterInteraction()
    {
        throw new System.NotImplementedException();
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
