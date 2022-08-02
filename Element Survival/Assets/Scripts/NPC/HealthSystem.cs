using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public float life = 100f;

    private float perpetualDamageQuantity;
    private float perpetualDamageTime;

    private Animator damageAnimation;


    // Start is called before the first frame update
    void Start() {
        damageAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(float damageQuantity)
    {
        damageAnimation.SetTrigger("Active");
        if (life - damageQuantity > 0) {
            life -= damageQuantity;
        }
        else {
            life = 0;
            Destroy(gameObject);
        }
        
    }
    
}
