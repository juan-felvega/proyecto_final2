using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    public Rigidbody2D rb;
    private float movimiento;
    public float velocidad = 5f;
    public bool faceingRight = true;
    public float jumpForce = 5f;
    public bool isGrounded = true;
    public Animator animator;
    public static bool jugadorSalto = false;

    // NUEVO: Control de movimiento externo
    public bool canMove = true;

    void Update()
    {
        if (!canMove)
        {
            movimiento = 0f;
            animator.SetFloat("Caminar", 0f);
            return;
        }

        movimiento = Input.GetAxis("Horizontal");

        // Flip del sprite
        if (movimiento < 0f && faceingRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            faceingRight = false;
        }
        else if (movimiento > 0f && !faceingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            faceingRight = true;
        }

        // Saltar
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
            animator.SetBool("Salto", true);
            jugadorSalto = true;
        }

        // Animaci�n caminar
        if (Mathf.Abs(movimiento) > 0f)
            animator.SetFloat("Caminar", 1f);
        else
            animator.SetFloat("Caminar", 0f);

        // Ataque
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Ataque");
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // reinicia velocidad Y antes de aplicar fuerza
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            isGrounded = true;
            animator.SetBool("Salto", false);
            jugadorSalto = false;
        }
    }
   

}
