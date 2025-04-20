using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia = 1f;
    [SerializeField] private LayerMask capaSuelo;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D infoSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia, capaSuelo);
        Debug.DrawRay(controladorSuelo.position, Vector2.down * distancia, Color.green);

        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        if (!infoSuelo.collider)
        {
            Girar();
        }
    }

    private void Girar()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        velocidad *= -1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.position, controladorSuelo.position + Vector3.down * distancia);
    }
}