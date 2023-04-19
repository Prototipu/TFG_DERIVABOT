using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Resta : Funcion
    {
        public Funcion Ux;
        public Funcion Vx;

        public Resta(Funcion uX, Funcion vX)
            : base(Type.Resta)
        {
            Ux = uX;
            Vx = vX;
        }

        public override Funcion Derivada()
        {
            return new Resta(Ux.Derivada(), Vx.Derivada());
        }

        public override object Clone()
        {
            return new Resta((Funcion)Ux.Clone(), (Funcion)Vx.Clone());
        }
    }
}
