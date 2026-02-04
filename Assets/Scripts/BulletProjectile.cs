using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    [SerializeField] private float speed = 20f;
    public float damage = 20;

    private bool hasHit = false;

    private Rigidbody bulletRigidbody;
    public BulletTarget bulletTarget;
    

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletRigidbody.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) {
            // do nothing
            }
        
        else if (other.GetComponent<BulletTarget>() != null)
        {
            // Hit target
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            bulletTarget = other.GetComponent<BulletTarget>();
            bulletTarget.GetHit(damage);
            hasHit = true;
        }
        else
        {
            // Hit something else
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
