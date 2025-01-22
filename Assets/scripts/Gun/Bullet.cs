using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private float homingSpeed = 5f;
    private LayerMask enemyLayer;
    private float homingRange;
    private Transform targetEnemy;

    // M�todo para establecer el da�o de la bala
    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    // M�todo para establecer los par�metros del homing
    public void SetHoming(LayerMask layer, float range)
    {
        enemyLayer = layer;
        homingRange = range;
    }

    void Update()
    {
        // Si hay un enemigo, la bala lo sigue
        if (targetEnemy != null)
        {
            Vector2 direction = (targetEnemy.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * homingSpeed;
        }
        else
        {
            // Si no hay objetivo, buscar un enemigo
            SearchForTarget();
        }
    }

    // Buscar al enemigo m�s cercano dentro del rango
    private void SearchForTarget()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, homingRange, enemyLayer);
        if (enemiesInRange.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            foreach (var enemy in enemiesInRange)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    targetEnemy = enemy.transform;
                }
            }
        }
    }

    // Cuando la bala colisiona con un enemigo
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar la colisi�n
        Debug.Log("Colisi�n con: " + collision.gameObject.name);

        // Verificar si la colisi�n es con un enemigo
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Obtener el componente EnemyHealth
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Aplicar el da�o
                Debug.Log("Aplicando da�o: " + damage);  // Agregar un log para verificar el da�o
                enemyHealth.TakeDamage(damage);
            }

            // Destruir la bala despu�s de impactar
            Destroy(gameObject);
        }
    }


}
