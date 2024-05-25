using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int puntosTotales = 0;
    public HUD HUD;
    public int vidas = 10;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public int PuntosTotales
    {
        get
        {
            return puntosTotales;
        }
        set
        {
            puntosTotales = value;
        }
    }


    public void SumarPuntos(int puntos)
    {
        puntosTotales += puntos;
        Debug.Log("Puntos: " + puntosTotales);
        HUD.ActualizarPuntos(puntosTotales);
    }

    public void RestarVida(int cantidad)
    {
        vidas -= cantidad;

        if (vidas <= 0)
        {
            // Aquí puedes hacer lo que desees cuando el jugador pierde todas las vidas,
            // por ejemplo, reiniciar la escena o mostrar un menú de juego over.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Actualiza la interfaz para reflejar la cantidad actual de vidas
        HUD.desactivarVida(vidas);
    }
    public bool RecuperarVida()
    {
        if(vidas == 10)
        {
            return false;
        }
        HUD.activarVida(vidas);
        vidas++;
        return true;
    }

}
