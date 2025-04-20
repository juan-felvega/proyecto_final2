using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject Jugador;

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = transform.position;
        Position.x = Jugador.transform.position.x;
        Position.y = Jugador.transform.position.y + 1.5f;
        transform.position = Position;
        
    }
}
