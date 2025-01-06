using TMPro;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 5f; // Velocidad del proyectil
        private Vector3 direction; // Dirección del proyectil
        private Vector3 targetPosition; // Posición del jugador

        public void Initialize(Vector3 direction)
        {
            this.direction = direction; // Almacena la dirección
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * speed; // Establece la velocidad del proyectil
            Destroy(gameObject, 2f); // Destruir el proyectil después de 2 segundos
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Manejar la colisión con el jugador
                Health playerHealth = other.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.Decrement(); // Inflige daño al jugador
                }
                Destroy(gameObject); // Destruir el proyectil al impactar
            }
        }

       

    }
}