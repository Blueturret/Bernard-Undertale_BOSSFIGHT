using UnityEngine;
public class TargetObstacle : Obstacle
{
    Transform playerPosition;

    protected override void Awake()
    {
        base.Awake();
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Mouvement de base de cet obstacle
    public override void OnObjectSpawned()
    { 
        Vector2 dir = playerPosition.position - transform.position;
        
        rb.linearVelocity = dir.normalized * speed;
    }
}
