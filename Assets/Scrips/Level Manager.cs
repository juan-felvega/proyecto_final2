using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Para cambio por trigger")]
    public string nombreEscenaTrigger;
   
    public void BotonStart()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonSalir()
    {
        Debug.Log("salir del juego");
            Application.Quit();
    }

    public void BotonTutorial()
    {
    SceneManager.LoadScene(2); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !string.IsNullOrEmpty(nombreEscenaTrigger))
        {
            SceneManager.LoadScene(nombreEscenaTrigger);
        }
    }
}
