using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public float saltosMaximos;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool mirandoDerecha = true;
    private float saltosRestantes;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;

    }
    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    bool EstaEnElSuelo()
    {
        RaycastHit2D raycastHit= Physics2D.BoxCast(bc.bounds.center, new Vector2(bc.bounds.size.x, bc.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    void ProcesarSalto()
    {
        if(EstaEnElSuelo())
        {
            saltosRestantes = saltosMaximos;
        }

        if(Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }   
    }

    void ProcesarMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);
        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
      if((mirandoDerecha && inputMovimiento < 0) || (!mirandoDerecha && inputMovimiento > 0))
        {
          mirandoDerecha = !mirandoDerecha;
          transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
      }
      
    }
}
