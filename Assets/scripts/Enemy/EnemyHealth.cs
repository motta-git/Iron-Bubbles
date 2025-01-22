using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;  // Salud del enemigo

    private void Start()
    {
        // Aquí puedes agregar cualquier inicialización si es necesario
        Debug.Log("Salud inicial del enemigo: " + health);
    }

    // Método para recibir daño
    public void TakeDamage(float amount)
    {
        health -= amount;  // Restamos la cantidad de daño de la salud
        Debug.Log("Salud restante: " + health);

        // Si la salud del enemigo es menor o igual a 0, el enemigo muere
        if (health <= 0)
        {
            Die();
        }
    }

    // Método que maneja la muerte del enemigo
    private void Die()
    {
        // Aquí puedes agregar animaciones, efectos de muerte, sonidos, etc.
        Debug.Log("Enemigo muerto");

        // Destruir el objeto enemigo (esto lo elimina del juego)
        Destroy(gameObject);
    }

    // Método para obtener la salud actual del enemigo (opcional)
    public float GetHealth()
    {
        return health;
    }

    // Si quieres manejar alguna lógica continua en el enemigo (por ejemplo, animaciones o efectos)
    private void Update()
    {
        // Aquí puedes agregar efectos visuales, animaciones, etc.
    }
}
