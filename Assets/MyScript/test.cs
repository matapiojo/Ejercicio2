using UnityEngine;

public class test : MonoBehaviour
{
    public float maxSpeed = 7f; // Velocidad m�xima de movimiento horizontal
    public float baseJumpSpeed = 5f; // Velocidad base de salto
    public float maxJumpMultiplier = 2f; // Multiplicador m�ximo para el salto seg�n la velocidad

    private float jumpSpeed;
    private bool isGrounded;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Verifica si el personaje est� en el suelo
        isGrounded = IsGrounded();

        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");

        // Flip de sprite seg�n la direcci�n del movimiento
        if (move > 0)
            spriteRenderer.flipX = false;
        else if (move < 0)
            spriteRenderer.flipX = true;

        // L�gica de salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Calcula el salto basado en la velocidad horizontal
            jumpSpeed = baseJumpSpeed + (Mathf.Abs(move) / maxSpeed) * maxJumpMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed); // Aplica el salto
        }
    }

    // M�todo para verificar si el personaje est� en el suelo
    bool IsGrounded()
    {
        // Usamos un raycast hacia abajo para comprobar si estamos tocando el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }
}
