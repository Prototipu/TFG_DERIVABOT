using System;
using UnityEngine;

namespace Derivadas_LIB
{
    public abstract class Funcion : MonoBehaviour, ICloneable
    {
        public Anclajes anclajes;
        public abstract Funcion Derivada();
        public abstract object Clone();
    }
}