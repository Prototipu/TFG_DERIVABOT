using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class robotController : MonoBehaviour
{
    public float bateria = 100f;
    public float energia = 50f;
    public TMP_Text textoBateria;
    public TMP_Text textoEnergia;
    public GameObject objetoOcultar;

    void Update()
    {

        // Actualizar los textos en el gameobject
        textoBateria.text = bateria.ToString();
        textoEnergia.text = energia.ToString();

        // Ocultar el gameobject si la batería llega a cero
        if (bateria <= 0f)
        {
            objetoOcultar.SetActive(false);
        }
    }
    void OnMouseDown()
    {
        // Reducir la batería en 1 cuando se hace clic en el gameobject
        bateria -= 1f;
        textoEnergia.text = energia.ToString();
        if (bateria <= 0f)
        {
            objetoOcultar.SetActive(false);
        }
    }
}
