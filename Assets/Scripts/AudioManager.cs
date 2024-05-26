using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * El audioSource se encarga de reproducir los sonidos del juego.
 */
[RequireComponent(typeof(AudioSource))] 

/*
 *Este script controla el sonido del juego.
*/
public class AudioManager : MonoBehaviour
{
    /*
     *audioSource: referencia al componente AudioSource.
     *Instance: instancia de la clase AudioManager.
     */
    private AudioSource audioSource;
    public static AudioManager Instance { get; private set;}


    /*
     *El método Awake se llama al inicio del juego, se encarga de asignar la instancia de la clase AudioManager.
     */
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
     *El método Start se llama al inicio del juego, se encarga de obtener la referencia al componente AudioSource.
     */
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    /*
     *Este método se encarga de reproducir un sonido.
     */

    public void ReproducirSonido(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);

    }
}
