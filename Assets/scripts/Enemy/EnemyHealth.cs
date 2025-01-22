using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;  // Salud del enemigo

    private void Start()
    {
        // Aqu� puedes agregar cualquier inicializaci�n si es necesario
        Debug.Log("Salud inicial del enemigo: " + health);
    }

    // M�todo para recibir da�o
    public void TakeDamage(float amount)
    {
        health -= amount;  // Restamos la cantidad de da�o de la salud
        Debug.Log("Salud restante: " + health);

        // Si la salud del enemigo es menor o igual a 0, el enemigo muere
        if (health <= 0)
        {
            Die();
        }
    }

    // M�todo que maneja la muerte del enemigo
    private void Die()
    {
        // Aqu� puedes agregar animaciones, efectos de muerte, sonidos, etc.
        Debug.Log("Enemigo muerto");

        // Destruir el objeto enemigo (esto lo elimina del juego)
        Destroy(gameObject);
    }

    // M�todo para obtener la salud actual del enemigo (opcional)
    public float GetHealth()
    {
        return health;
    }

    // Si quieres manejar alguna l�gica continua en el enemigo (por ejemplo, animaciones o efectos)
    private void Update()
    {
        // Aqu� puedes agregar efectos visuales, animaciones, etc.
    }
}
