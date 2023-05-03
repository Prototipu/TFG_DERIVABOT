using System;
using UnityEngine;

namespace Derivadas_LIB
{
    public abstract class Funcion : MonoBehaviour, ICloneable
    {
        public SpriteRenderer _espacio;

        private void Awake()
        {
            _espacio = GetComponent<SpriteRenderer>();
        }

        public abstract Bounds Escalar();
        public abstract Funcion Derivada();
        public abstract object Clone();
    }
}