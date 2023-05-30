using System;
using System.Collections.Generic;
using UnityEngine;

namespace Derivadas_LIB
{
    public abstract class Funcion : MonoBehaviour, ICloneable
    {
        public Funcion FuncionSuperior = null;

        public Anclajes anclajes;
        public abstract Funcion Derivada();
        public abstract object Clone();
        public abstract void Swap(Funcion oldFx, Funcion newFx);
    }
}