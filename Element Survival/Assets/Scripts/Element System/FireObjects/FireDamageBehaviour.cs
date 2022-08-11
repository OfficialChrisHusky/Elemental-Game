using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamageBehaviour : MonoBehaviour
{
    public float effectDamage = 25f;
    public float burnProbability = 25f;
    public float maxRadiusUpTo = 3f;
    public float startRadiusValue = 10f;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SphereCollider>().radius = startRadiusValue * Random.Range(1, maxRadiusUpTo);
        StartCoroutine(waitAndDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {

        StopAllCoroutines();
     
        if (collider.gameObject.tag.Equals("Killable"))
        {
            var health = collider.gameObject.GetComponent<HealthSystem>();
            health.damage(effectDamage);
            var dice = Random.Range(0f, 100f);
            if (dice <= burnProbability && dice >= 0) health.setHealthEffect(HealthSystem.HealthStatus.Status.BURNED);
        }

        Destroy(this.gameObject);
       
    }

    IEnumerator waitAndDestroy()
    {

        yield return new WaitForSeconds(90);

        Destroy(this.gameObject);

    }

}
