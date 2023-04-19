using System;
using UnityEngine;

namespace Derivadas_LIB
{
    public abstract class Funcion : MonoBehaviour,ICloneable
    {
        public enum Type
        {
            None,
            Suma,
            Resta,
            Multiplicacion,
            Division,
            Potencial,
            Exponencial,
            Logaritmica,
            Incognita
        }

        public Type Ftype;

        public abstract Funcion Derivada();
        public abstract object Clone();
    }
}