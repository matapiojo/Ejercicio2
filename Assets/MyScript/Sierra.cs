using Platformer.Gameplay;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Sierra : MonoBehaviour
    {
        public float speed = 2f; // Velocidad de movimiento
        public float moveDistance = 3f; // Distancia m�xima de movimiento
        private Vector2 startPosition; // Posici�n inicial de la sierra
        Health playerHealth;

        void Start()
        {
            startPosition = transform.position; // Guardar la posici�n inicial
        }

        void Update()
        {
            // Calcular la nueva posici�n
            float newX = startPosition.x + Mathf.Sin(Time.time * speed) * moveDistance;
            transform.position = new Vector2(newX, transform.position.y); // Mover solo en el eje X
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Verifica si el objeto que entr� en el trigger es el jugador
            if (other.CompareTag("Player"))
            {
                // Accede al script del jugador y reduce su salud
                playerHealth = other.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.Decrement(); 
                }
            }
        }
    }
}
