using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;

    private float rocketStrength = 15.0f;
    private float aliveTimer = 5.0f;
    
    // moving and rotating the missile towards the target.
    void Update()
    {
        if(homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position -
            transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    // This will be called by our player when we spawn in the rockets, so needs to be public.
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    // This will add a force to whatever is hit.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(target.tag))
        {
            Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
            Vector3 away = -col.contacts[0].normal;
            targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
