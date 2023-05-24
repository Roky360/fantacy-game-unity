using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] public bool playerInRange;
    [SerializeField] public GameObject objectToThrow;
    public int RotationRange = 360;
    public float throwForce = 1000;

    void ThrowWood()
    {
        // Instantiate the object to throw
        Rigidbody thrownObject = Instantiate(objectToThrow, transform.position, Quaternion.identity)
            .GetComponent<Rigidbody>();

        // Calculate a random direction around the parent object
        Quaternion randomRotation = Random.rotation;
        Debug.Log(randomRotation);

        // Set the position of the thrown object slightly above the parent object
        Vector3 throwPosition = transform.position + Vector3.up;

        // Set the position and rotation of the thrown object
        thrownObject.transform.position = throwPosition;
        thrownObject.transform.rotation = randomRotation;

        // Apply force to the object in the forward direction of its local space
        thrownObject.AddForce(thrownObject.transform.forward * throwForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            ThrowWood();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}