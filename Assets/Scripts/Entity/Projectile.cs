using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour 
{
    [SerializeField] private int damage;
    [SerializeField] private float force;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collider)
    {
        hit(collider.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hit(collision.gameObject);
    }

    private void hit(GameObject target)
    {
        var healthComponent = target.GetComponent<Health>();
        if(healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
        }

        // TODO: Maybe let's do some better management than an instant destroy.  And make cool effects!
        Destroy(gameObject);
    }
}
