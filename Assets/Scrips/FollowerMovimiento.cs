using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMovimiento : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 3f;
    public float distanciaMinima = 1.5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    public Animator animator;  
    public bool isGrounded = true;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (objetivo == null) return;

        float distancia = Vector2.Distance(transform.position, objetivo.position);
        Vector2 direccion = (objetivo.position - transform.position).normalized;

        // Movimiento
        if (distancia > distanciaMinima)
        {
            rb.velocity = new Vector2(direccion.x * velocidad, rb.velocity.y);

            if (isGrounded)
                animator.SetFloat("Caminar", 1f);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetFloat("Caminar", 0f);
        }

        // Imitar salto si el jugador salta
        // Solo salta si está por debajo del jugador (evita empujones locos)
        if (PlayerMovimiento.jugadorSalto && isGrounded && transform.position.y < objetivo.position.y - 0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Limpiar Y antes del salto
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("Salto", true);
        }

        // Limitar la velocidad vertical máxima (evita saltos gigantes)
        if (rb.velocity.y > 10f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }

        // Flip horizontal
        if ((direccion.x > 0 && !facingRight) || (direccion.x < 0 && facingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.eulerAngles = new Vector3(0f, facingRight ? 0f : 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            isGrounded = true;
            animator.SetBool("Salto", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            isGrounded = false;
        }
    }
}