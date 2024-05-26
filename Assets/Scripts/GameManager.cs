using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *Este script controla el flujo del juego. En términos de puntuación y vidas.
 */
public class GameManager : MonoBehaviour
{
    /*
     *puntosTotales: puntos totales del jugador.
     *HUD: referencia al HUD (Heads Up Display: Interfaz de usuario).
     *vidas: vidas del jugador.
     *Instance: instancia de la clase GameManager.
     */
    private int puntosTotales = 0;
    public HUD HUD;
    public int vidas = 10;
    public static GameManager Instance;


    /*
     *El método Awake se llama al inicio del juego, se encarga de asignar la instancia de la clase GameManager.
     */
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    /*
     *El método Start se llama al inicio del juego, se encarga de obtener la referencia al HUD.
     */
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

    /*
     *Este método se encarga de sumar los puntos al jugador.
     */
    public void SumarPuntos(int puntos)
    {
        puntosTotales += puntos;
        Debug.Log("Puntos: " + puntosTotales);
        HUD.ActualizarPuntos(puntosTotales);
    }

    /*
     *Este método se encarga de restar vidas al jugador.
     *Si las vidas llegan a 0, se reinicia la escena.
     */

    public void RestarVida(int cantidad)
    {
        vidas -= cantidad;

        if (vidas <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        HUD.desactivarVida(vidas);
    }

    /*
     *Este método se encarga de recuperar una vida.
     *Si el jugador tiene 10 vidas, no se puede recuperar más vidas.
     */
    public bool RecuperarVida()
    {
        if(vidas == 10)
        {
            return false;
        }
        vidas++;
        HUD.activarVida(vidas);
        return true;
    }

}
