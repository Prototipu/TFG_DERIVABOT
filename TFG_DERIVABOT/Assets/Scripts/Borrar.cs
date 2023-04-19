using Derivadas_LIB.Funciones;
using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Borrar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Incognita eX = new Incognita();
        eX.Init(3, 3);

        Logaritmica uX = new Logaritmica();
        Incognita temp = new Incognita();
        temp.Init(4, 5);
        uX.Init(temp);

        Exponencial vX = new Exponencial();
        vX.Init((Funcion)eX.Clone());

        Suma suma = new Suma();
        suma.Init(uX, vX);

        Multiplicacion mult = new Multiplicacion();
        mult.Init(eX, suma);

        Potencial p = new Potencial();
        p.Init(5, (Funcion)eX.Clone(), 3);

        Division div = new Division();
        div.Init(mult, p);


        Debug.Log(ParserFunciones.ParsearString(div, Funcion.Type.None));

        Debug.Log(ParserFunciones.ParsearString(div.Derivada(), Funcion.Type.None));
    }

    // Update is called once per frame
}
