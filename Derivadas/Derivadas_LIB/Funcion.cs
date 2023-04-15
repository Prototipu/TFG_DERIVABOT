using System;

namespace Derivadas_LIB
{
    public abstract class Funcion : ICloneable
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

        public Funcion(Type type)
        {
            Ftype = type;
        }

        public abstract Funcion Derivada();
        public abstract object Clone();
    }
}