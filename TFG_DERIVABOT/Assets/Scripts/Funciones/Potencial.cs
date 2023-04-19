using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Potencial : Funcion
    {
        public int Exponente;

        public Funcion Fx;

        public int K;

        public Potencial(int k, Funcion fX, int exponente)
            : base(Type.Potencial)
        {
            Exponente = exponente;
            Fx = fX;
            K = k;
        }

        public override Funcion Derivada()
        {
            Potencial p = new Potencial(K * Exponente, (Funcion)Fx.Clone(), Exponente - 1);

            Multiplicacion m = new Multiplicacion(p, Fx.Derivada());
            
            return m;
        }

        public override object Clone()
        {
            Potencial p = new Potencial(K,(Funcion)Fx.Clone(), Exponente);

            return p;
        }
    }
}
