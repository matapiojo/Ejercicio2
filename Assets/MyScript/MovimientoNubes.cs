using UnityEngine;

public class MovimientoNubes : MonoBehaviour
{
    public float speed = 1f; // Velocidad del movimiento
    public float resetPosition = -10f; // Posici�n para reiniciar la nube

    void Update()
    {
        // Mueve la nube hacia la izquierda
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Reinicia la posici�n de la nube si sale de la pantalla
        if (transform.position.x < resetPosition)
        {
            // Ajusta aqu� la nueva posici�n (puedes cambiar el valor seg�n necesites)
            transform.position = new Vector3(10f, transform.position.y, transform.position.z);
        }
    }
}