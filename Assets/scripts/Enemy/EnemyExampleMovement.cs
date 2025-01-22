using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularEnemyMovement : MonoBehaviour
{
    // Movement options
    public enum MovementType { FollowTarget, Roaming, PathFollowing }
    [SerializeField] private MovementType movementType = MovementType.FollowTarget;

    // Target and path for movement
    [SerializeField] private Transform target; // The target to follow
    [SerializeField] private float speed = 5f; // Speed of movement
    [SerializeField] private Vector3[] pathPoints; // Predefined path points for path following

    // Movement Range
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 30f;

    // Shooting settings
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float shootInterval = 0.5f;
    private float lastShootTime;

    // Health and other functionalities
    [SerializeField] private float health = 100f;
    private bool isDead = false;

    private int currentPathIndex = 0;

    void Start()
    {
        lastShootTime = Time.time;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleShooting();
    }

    // Movement logic based on selected movement pattern
    private void HandleMovement()
    {
        switch (movementType)
        {
            case MovementType.FollowTarget:
                MoveTowardsTarget();
                break;

            case MovementType.Roaming:
                RoamRandomly();
                break;

            case MovementType.PathFollowing:
                FollowPath();
                break;
        }
    }

    // Move towards the target (standard)
    private void MoveTowardsTarget()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        // Only move if within specified range
        if (distance < minDistance && distance > maxDistance)
        {
            Vector3 targetPosition = target.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    // Roam randomly in the area
    private void RoamRandomly()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        transform.position += randomDirection * speed * Time.deltaTime;
    }

    // Follow a predefined path of waypoints
    private void FollowPath()
    {
        if (pathPoints.Length == 0) return;

        Vector3 targetPosition = pathPoints[currentPathIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPathIndex = (currentPathIndex + 1) % pathPoints.Length;
        }
    }

    // Handle shooting at a regular interval
    private void HandleShooting()
    {
        if (Time.time - lastShootTime >= shootInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    // Shoot bullet
    private void Shoot()
    {
        if (enemyBullet != null)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            Debug.Log("Enemy shot a bullet!");
        }
    }

    // Take damage (example for health system)
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    // Handle death (destroy the enemy)
    private void Die()
    {
        isDead = true;
        Debug.Log("Enemy is dead!");
        Destroy(gameObject);  // Destroy enemy on death
    }
}

