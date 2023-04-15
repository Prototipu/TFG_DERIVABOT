using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Exponencial : Funcion
    {
        public Funcion Fx;

        public Exponencial(Funcion fX)
            : base(Type.Exponencial)
        {
            Fx = fX;
        }

        public override Funcion Derivada()
        {
            Exponencial e = new Exponencial((Funcion)Fx.Clone());

            Multiplicacion m = new Multiplicacion(Fx.Derivada(), e);

            return m;
        }

        public override object Clone()
        {
            Exponencial p = new Exponencial((Funcion)Fx.Clone());

            return p;
        }
    }
}
