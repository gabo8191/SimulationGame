using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI txtPuntos;
    public GameObject[] vidas;

    void Update()
    {
        txtPuntos.text = "Puntos: " + gameManager.PuntosTotales.ToString();
    }

    public void ActualizarPuntos(int puntos)
    {
        txtPuntos.text = "Puntos: " + puntos.ToString();
    }

    public void desactivarVida(int vidasRestantes)
    {
        // Asegurar que todos los corazones están activos antes de desactivar los necesarios
        foreach (GameObject corazon in vidas)
        {
            corazon.SetActive(true);
        }

        // Desactivar corazones desde el primero hasta el último, basado en las vidas restantes
        for (int i = vidas.Length - 1; i >= vidasRestantes; i--)
        {
            if (i >= 0 && i < vidas.Length)
            {
                vidas[i].SetActive(false);
            }
        }
    }

    public void activarVida(int vidasActuales)
    {
        // Activar el corazón correspondiente a la próxima vida recuperada
        if (vidasActuales <= vidas.Length)
        {
            vidas[vidasActuales - 1].SetActive(true);
        }
    }

}
