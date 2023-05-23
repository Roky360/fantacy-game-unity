using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitManager : MonoBehaviour
{
    [SerializeField] float reachDistance = 4;
    [SerializeField] float radius = 0.5f;
    [SerializeField] LayerMask interactableLayerMask;
    [SerializeField] bool onTarget;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        onTarget = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        // Physics.Raycast(_rb.position, transform.forward, out hit, reachDistance, interactableLayerMask);
        if (Physics.SphereCast(_rb.position, radius, transform.forward, out hit, reachDistance /*, interactableLayerMask*/))
        {
            InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
            if (interactableObject && interactableObject.playerInRange)
            {
                onTarget = true;
            }
            else
            {
                onTarget = false;
            }
        }
        else
        {
            onTarget = false;
        }
    }
}