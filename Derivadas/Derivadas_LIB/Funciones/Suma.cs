using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Suma : Funcion
    {
        public Funcion Ux;
        public Funcion Vx;

        public Suma(Funcion uX, Funcion vX)
            : base(Type.Suma)
        {
            Ux = uX;
            Vx = vX;
        }

        public override Funcion Derivada()
        {
            return new Suma(Ux.Derivada(), Vx.Derivada());
        }

        public override object Clone()
        {
            return new Suma((Funcion)Ux.Clone(), (Funcion)Vx.Clone());
        }
    }
}
