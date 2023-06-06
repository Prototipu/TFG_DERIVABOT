using Derivadas_LIB.Funciones;
using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Borrar : MonoBehaviour
{

    public SpriteRenderer space;

    private Funcion f;

    // Start is called before the first frame update
    void Start()
    {

        // DIV SUM X 3 2 X 1 5 RES X 2 1 X 4 6
        // (3x^2 + x^5)/(2x - 4x^6)

        // DIV DIV SUM X 3 2 X 2 4 MUL SUM X 5 2 X 3 1 RES X 4 8 X 3 2 X 2 5

        // LOG EXP SUM X 3 5 POT 2 5 X 2 3
        // Ln(e^(3x^5 + 2*(2x^3)^5)

        // SUM X 3 2 X 4 5
        // 3x^2 + 4x^5

        // SUM X 2 3 EXP SUM MUL X 2 3 SUM X 2 3 X 3 1 DIV SUM X 3 2 X 1 5 RES X 2 1 X 4 6

        // MUL DIV X 2 3 SUM X 3 2 X 3 2 RES X 2 3 X 2 3

        ManagerFunciones.Instance.InitNivel("MUL DIV MUL X 2 3 X 2 0 SUM X 3 2 X 3 2 RES X 2 0 X 2 0");

        //ManagerHerramientas.Instance.Clonacion.OnSalir += Clonacion_OnSalir;

        Invoke("Test", 0.5f);
    }

    private void Clonacion_OnSalir()
    {
        Invoke("Test", 0.5f);
    }

    private void Test()
    {
        //ManagerHerramientas.Instance.IniciarHerramienta(ManagerHerramientas.EHerramienta.Clonacion);

        ManagerHerramientas.Instance.IniciarHerramienta(ManagerHerramientas.EHerramienta.Reciclaje);
    }
}
