using System;
using UnityEngine;

namespace Derivadas_LIB
{
    public abstract class Funcion : MonoBehaviour, ICloneable
    {
        public SpriteRenderer _espacio, _extra;

        public abstract Vector2 Escalar();

        public abstract Funcion Derivada();
        public abstract object Clone();
    }
}