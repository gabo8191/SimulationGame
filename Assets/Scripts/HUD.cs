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

    public void desactivarVida(int vida)
    {
        vidas[vida].SetActive(false);
        
    }

    public void activarVida(int vida)
    {
        vidas[vida].SetActive(true);
    }

}
