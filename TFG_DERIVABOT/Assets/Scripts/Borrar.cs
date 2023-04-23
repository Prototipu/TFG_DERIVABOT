using Derivadas_LIB.Funciones;
using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Borrar : MonoBehaviour
{

    public SpriteRenderer space;
    
    // Start is called before the first frame update
    void Start()
    {

        // DIV SUM F 3 2 F 1 5 RES F 2 1 F 4 6

        // (3x^2 + x^5)/(2x - 4x^6)


        //Incognita eX = ManagerFunciones.Instance.GetFuncion<Incognita>();
        //eX.Init(3, 3);

        //Logaritmica uX = ManagerFunciones.Instance.GetFuncion<Logaritmica>();
        //Incognita temp = ManagerFunciones.Instance.GetFuncion<Incognita>();
        //temp.Init(4, 5);
        //uX.Init(temp);

        //Exponencial vX = ManagerFunciones.Instance.GetFuncion<Exponencial>();
        //vX.Init((Funcion)eX.Clone());

        //Suma suma = ManagerFunciones.Instance.GetFuncion<Suma>();
        //suma.Init(uX, vX);

        //Multiplicacion mult = ManagerFunciones.Instance.GetFuncion<Multiplicacion>();
        //mult.Init(eX, suma);

        //Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>();
        //p.Init(5, (Funcion)eX.Clone(), 3);

        //Division div = ManagerFunciones.Instance.GetFuncion<Division>();
        //div.Init(mult, p);

        //div.name = "Original";

        //div.Escalar(space);

        //Funcion derivada = div.Derivada();

        //derivada.name = "Derivada";

        //derivada.Escalar(space);

        //Debug.Log(ParserFunciones.ParsearString(div, null));

        //Destroy(div.gameObject);
        //Debug.Log(ParserFunciones.ParsearString(derivada, null));


        Funcion f = ParserFunciones.CrearFuncion("DIV SUM F 3 2 F 1 5 RES F 2 1 F 4 6");

        f.Escalar(space);

        Debug.Log(ParserFunciones.ParsearString(f, null));

    }

    // Update is called once per frame
}
