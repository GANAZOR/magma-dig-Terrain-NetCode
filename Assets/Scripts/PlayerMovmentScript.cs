using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento3D : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float alturaSalto = 5.0f;

    private Rigidbody rb;

    private Transform cam;

    private bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Obtén la dirección de la cámara
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movimiento = (movimientoVertical * camForward + movimientoHorizontal * cam.right) * velocidad;

        rb.velocity = new Vector3(movimiento.x, rb.velocity.y, movimiento.z);

        // Saltar si se presiona la tecla de espacio y el jugador está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.AddForce(Vector3.up * alturaSalto, ForceMode.Impulse);
            enSuelo = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            enSuelo = true;
        }
    }
}

