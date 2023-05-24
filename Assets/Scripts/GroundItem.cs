using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    /// <summary>
    /// Time before despawn, in seconds
    /// </summary>
    [SerializeField] float timeToLive = 3;
    /// <summary>
    /// If the player is in range, the item can be picked
    /// </summary>
    public bool pickable;

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickable = false;
        }
    }
}