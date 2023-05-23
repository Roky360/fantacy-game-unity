using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] public LayerMask ground;
    [SerializeField] public float lifetime = 5.0f;
    [SerializeField] public float maxDistance;

    // How many enemies the projectile can pierce through
    [SerializeField] public uint pierceAmount = 0;

    // TODO: Add projectile damage?

    private Rigidbody _rb;
    private bool _reduceLifetime;

    private float _distanceTraveled;
    private Vector3 _startPosition;

    void StopProjectile()
    {
        _rb.velocity = Vector3.zero;
        _reduceLifetime = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StopProjectile();
        }
        else if (other.CompareTag("Enemy"))
        {
            /* DEAL DAMAGE TO ENEMY */

            if (pierceAmount-- <= 0)
            {
                StopProjectile();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _reduceLifetime = false;

        _distanceTraveled = 0f;
        _startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (!_reduceLifetime)
        {
            // Update the distance traveled
            _distanceTraveled = Vector3.Distance(_startPosition, transform.position);

            // Check if the projectile has exceeded the maximum distance
            if (_distanceTraveled >= maxDistance)
            {
                StopProjectile();
            }
            
            // // destroy when hit a Ground object
            // // if (Physics.CheckSphere(_rb.position, 0.1f, ground))
            // if (Physics.CheckSphere(_rb.position, gameObject.transform.localScale.x + 0.1f, ground))
            // {
            //     StopProjectile();
            // }
            // else
            // {
            //     // Update the distance traveled
            //     _distanceTraveled = Vector3.Distance(_startPosition, transform.position);
            //
            //     // Check if the projectile has exceeded the maximum distance
            //     if (_distanceTraveled >= maxDistance)
            //     {
            //         StopProjectile();
            //     }
            // }
        }
    }

    private void Update()
    {
        if (_reduceLifetime)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}