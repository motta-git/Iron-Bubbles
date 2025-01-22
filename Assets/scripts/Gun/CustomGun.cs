using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGun : MonoBehaviour
{
    [Header("Configuración del disparo")]
    [SerializeField] GameObject bulletPrefab; // Prefab de la bala
    [SerializeField] Transform shootPoint; // Punto de disparo
    [SerializeField] float bulletSpeed = 10f; // Velocidad de las balas
    [SerializeField] float fireRate = 0.2f; // Frecuencia de disparo (segundos entre disparos)
    [SerializeField] int bulletsPerShot = 1; // Número de balas disparadas por disparo
    [SerializeField] float spreadAngle = 5f; // Ángulo de dispersión de las balas
    [SerializeField] bool autoFire = false; // Disparo automático (si es verdadero, el arma dispara continuamente)
    [SerializeField] bool homing = false; // Las balas seguirán a los enemigos
    [SerializeField] float homingRange = 10f; // Rango para detectar enemigos (si es homing)
    [SerializeField] LayerMask enemyLayer; // Capa de enemigos
    [SerializeField] float bulletDamage = 10f; // Daño de las balas

    private float nextFireTime = 0f; // Tiempo de espera para el siguiente disparo

    void Update()
    {
        if (autoFire)
        {
            // Disparar continuamente mientras el jugador mantiene el botón de disparo
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
            {
                Shoot();
            }
        }
        else
        {
            // Disparar solo cuando se presiona el botón de disparo
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime)
            {
                Shoot();
            }
        }

        RotateGun(); // Rotar el arma hacia el mouse
    }

    // Método para disparar las balas
    void Shoot()
    {
        // Establecer el próximo tiempo de disparo basado en la tasa de disparo
        nextFireTime = Time.time + fireRate;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            // Calcular el ángulo de dispersión
            float angle = Random.Range(-spreadAngle, spreadAngle);

            // Crear el proyectil en el punto de disparo
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            // Obtener la dirección hacia el mouse
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = new Vector2(mousePosition.x - shootPoint.position.x, mousePosition.y - shootPoint.position.y);

            // Aplicar dispersión al ángulo de disparo
            direction = RotateVector(direction, angle);

            // Normalizar la dirección y asignar la velocidad
            direction.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            // Si las balas siguen a un enemigo, asignar comportamiento homing
            if (homing)
            {
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.SetHoming(enemyLayer, homingRange); // Asignar el comportamiento homing
                }
            }

            // Establecer el daño de la bala
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetDamage(bulletDamage);
            }
        }
    }


    // Método para rotar el arma hacia el mouse
    void RotateGun()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction; // Hacer que el arma apunte hacia el mouse
    }

    // Método para rotar un vector 2D en torno al eje Z
    Vector2 RotateVector(Vector2 originalVector, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float cosAngle = Mathf.Cos(radian);
        float sinAngle = Mathf.Sin(radian);

        float xNew = originalVector.x * cosAngle - originalVector.y * sinAngle;
        float yNew = originalVector.x * sinAngle + originalVector.y * cosAngle;

        return new Vector2(xNew, yNew);
    }
}
