using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Mechanics
{
    public class Moneda : MonoBehaviour
    {
        public int score = 1;
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Verifica si el objeto que entró en el trigger es el jugador
            if (other.CompareTag("Player"))
            {
                // Aumentar el puntaje
                ScoreManager.Instance.AddScore(score); // Aumenta el puntaje en 1
                Destroy(gameObject); // Destruye la moneda
            }
        }
    }
}