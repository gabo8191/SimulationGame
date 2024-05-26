using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *Este script controla el HUD del juego. El HUD es importante para mostrar información al jugador.
 */

public class HUD : MonoBehaviour
{

    /*
     *gameManager: referencia al componente GameManager.
     *txtPuntos: referencia al componente TextMeshProUGUI, una extensión de TextMeshPro que permite trabajar con texto en Unity.
     *vidas: arreglo de GameObjects. Cada GameObject representa una vida.
     */
    public GameManager gameManager;
    public TextMeshProUGUI txtPuntos;
    public GameObject[] vidas;

    /*
     *El método Start se llama al inicio del juego. Donde se obtiene la referencia al componente TextMeshProUGUI y se actualiza el texto.
     */
    void Update()
    {
        txtPuntos.text = "Puntos: " + gameManager.PuntosTotales.ToString();
    }

    /*
     *Este método se encarga de actualizar los puntos en el HUD.
     */

    public void ActualizarPuntos(int puntos)
    {
        txtPuntos.text = "Puntos: " + puntos.ToString();
    }

    /*
     *Este método se encarga de desactivar las vidas en el HUD.
     *Si el jugador pierde una vida, se desactiva una vida en el HUD.
     */

    public void desactivarVida(int vidasRestantes)
    {
        foreach (GameObject corazon in vidas)
        {
            corazon.SetActive(true);
        }
        for (int i = vidas.Length - 1; i >= vidasRestantes; i--)
        {
            if (i >= 0 && i < vidas.Length)
            {
                vidas[i].SetActive(false);
            }
        }
    }

    /*
     *Este método se encarga de activar las vidas en el HUD.
     *Si el jugador gana una vida, se activa una vida en el HUD.
     */

    public void activarVida(int vidasActuales)
    {
        if (vidasActuales <= vidas.Length)
        {
            vidas[vidasActuales - 1].SetActive(true);
        }
    }

}
