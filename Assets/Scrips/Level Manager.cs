using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
   
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


}
