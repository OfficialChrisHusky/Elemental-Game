using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            collision.gameObject.GetComponent<HealthSystem>().damage(damage);
        }
        Destroy(this.transform.parent.gameObject);
    }
}
