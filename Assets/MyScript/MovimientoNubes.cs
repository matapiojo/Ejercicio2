using UnityEngine;

public class MovimientoNubes : MonoBehaviour
{
    public float speed = 1f; // Velocidad del movimiento
    public float resetPosition = -10f; // Posición para reiniciar la nube

    void Update()
    {
        // Mueve la nube hacia la izquierda
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Reinicia la posición de la nube si sale de la pantalla
        if (transform.position.x < resetPosition)
        {
            // Ajusta aquí la nueva posición (puedes cambiar el valor según necesites)
            transform.position = new Vector3(10f, transform.position.y, transform.position.z);
        }
    }
}