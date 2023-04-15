using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Logaritmica : Funcion
    {
        public Funcion Fx;

        public Logaritmica(Funcion fX)
            : base(Type.Logaritmica)
        {
            Fx = fX;
        }

        public override Funcion Derivada()
        {
            return new Division(Fx.Derivada(), (Funcion)Fx.Clone());
        }

        public override object Clone()
        {
            Logaritmica p = new Logaritmica((Funcion)Fx.Clone());

            return p;
        }
    }
}
