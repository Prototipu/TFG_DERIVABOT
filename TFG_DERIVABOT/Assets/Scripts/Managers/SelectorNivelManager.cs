using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNivelManager : MonoBehaviour
{
    public void SeleccionarNivel(string nivel)
    {
        GameManager.Instance.CargarNivel(nivel);
    }
}
