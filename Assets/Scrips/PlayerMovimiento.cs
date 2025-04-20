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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movimiento = Input.GetAxis("Horizontal");

        if (movimiento < 0f && faceingRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            faceingRight = false;
            
        }
        else if (movimiento > 0f && faceingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            faceingRight = true;
            
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
            animator.SetBool("Salto", true);
            jugadorSalto = true;
        }

        if (Mathf.Abs(movimiento) > 0f){
            animator.SetFloat("Caminar", 1f);
        }
        else if (movimiento < 0.01f){
            animator.SetFloat("Caminar", 0f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Ataque");
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);

    }
    void Jump()
    {
    rb.velocity = new Vector2(rb.velocity.x, 0f); // reinicia velocidad Y antes de aplicar fuerza
    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag ("Piso"))
        {
            isGrounded = true;
            animator.SetBool("Salto", false);
            jugadorSalto = false;
        }
    }
}
