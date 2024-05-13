using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int puntosTotales = 0;
    public HUD HUD;
    public int vidas = 3;

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

    public void RestarVida()
    {
        vidas--;

        if(vidas == 0)
        {
            SceneManager.LoadScene(0);
        }

        HUD.desactivarVida(vidas);
    }

    public bool RecuperarVida()
    {
        if(vidas == 3)
        {
            return false;
        }
        HUD.activarVida(vidas);
        vidas++;
        return true;
    }

}
