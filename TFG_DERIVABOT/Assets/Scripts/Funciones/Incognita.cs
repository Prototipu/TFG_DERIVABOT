using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Incognita : Funcion
    {
        public int Exponente;

        public int K;

        public Incognita(int k, int exponente)
            : base(Type.Incognita)
        {
            Exponente = exponente;
            K = k;
        }

        public override Funcion Derivada()
        {
            return new Incognita(K * Exponente, Exponente - 1);
        }

        public override object Clone()
        {
            return new Incognita(K, Exponente);
        }
    }
}
