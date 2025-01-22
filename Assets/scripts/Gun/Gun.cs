using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; // Prefab del proyectil
    [SerializeField] float bulletSpeed = 10f; // Velocidad del proyectil
    [SerializeField] Transform shootPoint; // Punto desde donde el proyectil será disparado

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Llamar al método de disparo cuando se presiona la tecla "S"
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        RotateGun(); // Método para rotar la pistola hacia el mouse
    }

    // Método para disparar el proyectil
    void Shoot()
    {
        // Crear el proyectil en la posición del punto de disparo
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // Obtener la posición del mouse en el mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calcular la dirección del mouse en relación al punto de disparo
        Vector2 direction = new Vector2(mousePosition.x - shootPoint.position.x, mousePosition.y - shootPoint.position.y);

        // Normalizar la dirección y aplicar la velocidad al proyectil
        direction.Normalize();
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    // Método para rotar la pistola hacia el mouse
    void RotateGun()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calcular la dirección entre el jugador y el mouse
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        // Hacer que la pistola se oriente en la dirección del mouse
        transform.up = direction;
    }
}
