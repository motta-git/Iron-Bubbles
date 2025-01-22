using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; // Prefab del proyectil
    [SerializeField] float bulletSpeed = 10f; // Velocidad del proyectil
    [SerializeField] Transform shootPoint; // Punto desde donde el proyectil ser� disparado

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Llamar al m�todo de disparo cuando se presiona la tecla "S"
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        RotateGun(); // M�todo para rotar la pistola hacia el mouse
    }

    // M�todo para disparar el proyectil
    void Shoot()
    {
        // Crear el proyectil en la posici�n del punto de disparo
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // Obtener la posici�n del mouse en el mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calcular la direcci�n del mouse en relaci�n al punto de disparo
        Vector2 direction = new Vector2(mousePosition.x - shootPoint.position.x, mousePosition.y - shootPoint.position.y);

        // Normalizar la direcci�n y aplicar la velocidad al proyectil
        direction.Normalize();
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    // M�todo para rotar la pistola hacia el mouse
    void RotateGun()
    {
        // Obtener la posici�n del mouse en el mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calcular la direcci�n entre el jugador y el mouse
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        // Hacer que la pistola se oriente en la direcci�n del mouse
        transform.up = direction;
    }
}
