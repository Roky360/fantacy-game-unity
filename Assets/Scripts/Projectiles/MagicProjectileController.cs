using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MagicProjectileController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float shootForce = 2000;
    [SerializeField] float lifetime = 5.0f;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] KeyCode trigger = KeyCode.Mouse0;
    [SerializeField] LayerMask ground;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(trigger))
        {
            Quaternion rotation = _player.transform.rotation;
            rotation.z = -10;
            rotation.x = 0;
            // rotation.y = 0;
            Vector3 position = _player.transform.position;
            position.y += .8f;
            position += _player.transform.forward;

            GameObject projectileInstance = Instantiate(projectile, position, rotation);

            ProjectileController projectileController = projectileInstance.AddComponent<ProjectileController>();
            projectileController.ground = ground;
            projectileController.lifetime = lifetime;
            projectileController.maxDistance = maxDistance;

            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            projectileRb.velocity = new Vector3(projectileRb.velocity.x, 0, projectileRb.velocity.z);
            projectileRb.AddForce(_player.transform.forward * shootForce);
        }
    }
}