using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoming : MonoBehaviour
{
    [SerializeField] float homingSpeed = 5f; // Velocidad con la que la bala sigue al enemigo
    [SerializeField] LayerMask enemyLayer; // Capa de enemigos
    [SerializeField] float homingRange = 10f; // Rango para detectar enemigos

    private Transform targetEnemy; // El objetivo (enemigo) hacia el que la bala se dirige

    void Start()
    {
        // Llamamos a la búsqueda del objetivo inmediatamente al iniciar
        SearchForTarget();
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            // Dirección hacia el objetivo
            Vector2 direction = (targetEnemy.position - transform.position).normalized;
            // Aplicamos la velocidad de homing a la bala
            GetComponent<Rigidbody2D>().velocity = direction * homingSpeed;
        }
        else
        {
            // Si no hay objetivo, buscamos un enemigo
            SearchForTarget();
        }
    }

    // Buscar al enemigo más cercano dentro del rango
    void SearchForTarget()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, homingRange, enemyLayer);

        if (enemiesInRange.Length > 0)
        {
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (var enemy in enemiesInRange)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                // Si encontramos un enemigo más cercano, lo seleccionamos
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.transform;
                }
            }

            // Asignamos el enemigo más cercano como objetivo
            targetEnemy = closestEnemy;
        }
    }

    // Cuando la bala colisiona con un enemigo
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si la colisión fue con un enemigo (verificando la capa)
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Aplicar daño al enemigo
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10f); // Puedes modificar esto para usar una variable de daño
            }

            Destroy(gameObject); // Destruir la bala después de impactar
        }
    }
}
