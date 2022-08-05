using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour
{
    public float speed = 3f;
    public float damage = 25f;
    
    private Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Killable")) {
            var health = collision.gameObject.GetComponent<HealthSystem>();
            health.damage(damage);
            var dice = Random.Range(0f, 100f);
            if(dice <= 100 && dice >= 0) health.setHealthEffect(HealthSystem.HealthStatus.Status.BURNED); 
        }
        Destroy(transform.parent.gameObject);
    }

    public void multiplyDamage(float factor)
    {
        damage *= factor;
    }
}
