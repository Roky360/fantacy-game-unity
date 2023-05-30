using Enemies_AI;
using UnityEngine;

public class AngryLog : EnemyAIController
{
    private GameObject _particles;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
        _particles = transform.Find("Particles").gameObject;
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        _particles.SetActive(isMoving);
    }
}