using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportTarget; // Punto B al que se teletransportará

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.CompareTag("Player")) // Asegúrate de que tu personaje tenga la etiqueta "Player"
        {
            // Teletransporta al jugador
            other.transform.position = teleportTarget.position;
        }
    }
}
