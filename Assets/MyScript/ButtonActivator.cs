using UnityEngine;

namespace Platformer.Mechanics
{
    public class ButtonActivator : MonoBehaviour
    {
        public GameObject[] platforms; // Plataformas a activar
        private bool isActive = false; // Estado del botón

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Activa las plataformas si el jugador entra en el trigger
                if (!isActive)
                {
                    ActivatePlatforms();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Desactiva las plataformas si el jugador sale del trigger
                if (isActive)
                {
                    DeactivatePlatforms();
                }
            }
        }

        private void ActivatePlatforms()
        {
            foreach (GameObject platform in platforms)
            {
                platform.SetActive(true); // Activa la plataforma
            }
            isActive = true;
        }

        private void DeactivatePlatforms()
        {
            foreach (GameObject platform in platforms)
            {
                platform.SetActive(false); // Desactiva la plataforma
            }
            isActive = false;
        }
    }
}