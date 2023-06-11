using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ControladorC : MonoBehaviour
{
    public abstract void EncajarFuncion(Funcion funcion, bool izquierda);

    public abstract Transform GetPunto(Punto punto);

    public abstract float Altura();

    public abstract float Anchura();
}