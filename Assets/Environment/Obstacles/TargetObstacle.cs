using System.Collections;
using UnityEngine;
public class TargetObstacle : Obstacle
{
    Transform playerPosition;
    Vector2 dir;

    protected override void Awake()
    {
        base.Awake();
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Mouvement de base de cet obstacle
    public override void OnObjectSpawned()
    {
        dir = playerPosition.position - transform.position;

        // Faire en sorte que le baton regarde le joueur
        float targetAngle = Mathf.Atan2(playerPosition.position.y - transform.position.y, playerPosition.position.x - transform.position.x);
        transform.rotation = Quaternion.AngleAxis(targetAngle * Mathf.Rad2Deg, Vector3.forward);

        StartCoroutine(LaunchProjectile(.7f));
    }

    IEnumerator LaunchProjectile(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        rb.linearVelocity = dir.normalized * speed;
    }
}
