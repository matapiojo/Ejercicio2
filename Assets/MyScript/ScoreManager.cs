using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Mechanics
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance; // Instancia singleton
        public int score; // Puntaje actual
        public TextMeshProUGUI textScore;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // No destruir al cargar nuevas escenas
            }
            else
            {
                Destroy(gameObject); // Asegurar que solo haya una instancia
            }
        }

        public void AddScore(int amount)
        {
            score += amount; // Aumentar el puntaje
            UpdateScoreUI(); // Actualiza el UI del puntaje
        }

        private void UpdateScoreUI()
        {
            if (textScore != null)
            {
                textScore.text = "Score: " + score;
            }
        }
    }
}