using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionCamara : MonoBehaviour
{
    public float velocidadRotacion = 2.0f;
    public Transform objetivo;

    private float rotX = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float inputMouseX = Input.GetAxis("Mouse X");
        float inputMouseY = Input.GetAxis("Mouse Y");

        rotX -= inputMouseY * velocidadRotacion;
        rotX = Mathf.Clamp(rotX, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        objetivo.transform.Rotate(Vector3.up * inputMouseX * velocidadRotacion);
    }
}

