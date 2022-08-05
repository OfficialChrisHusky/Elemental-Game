using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour
{
 
    private new Rigidbody rigidbody;

    [Header("Fire Projectile Variables")]
    public int burningPercentage = 20;
    public float projectileSpeed = 1f;
    public float projectileDamage = 25f;
    public bool invertForwardDirection = true;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invertForwardDirection)
        {
            rigidbody.velocity = -transform.forward * projectileSpeed;
        }
        else
        {
            rigidbody.velocity = transform.forward * projectileSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Killable")) {
            var health = collision.gameObject.GetComponent<HealthSystem>();
            health.damage(projectileDamage);
            var dice = Random.Range(0f, 100f);
            if(dice <= burningPercentage && dice >= 0) health.setHealthEffect(HealthSystem.HealthStatus.Status.BURNED); 
        }
        Destroy(transform.parent.gameObject);
    }

    public void multiplyDamage(float factor)
    {
        projectileDamage *= factor;
    }
}
