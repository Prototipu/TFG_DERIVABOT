using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Multiplicacion : Funcion
    {
        public Funcion Ux;
        public Funcion Vx;

        public Multiplicacion(Funcion uX, Funcion vX)
            : base(Type.Multiplicacion)
        {
            Ux = uX;
            Vx = vX;
        }

        public override Funcion Derivada()
        {
            Funcion dUx = Ux.Derivada();
            Funcion dVx = Vx.Derivada();

            Multiplicacion m1 = new Multiplicacion(dUx, (Funcion)Vx.Clone());
            Multiplicacion m2 = new Multiplicacion((Funcion)Ux.Clone(), dVx);

            Suma s = new Suma(m1, m2);

            return s;
        }

        public override object Clone()
        {
            Multiplicacion m = new Multiplicacion((Funcion)Ux.Clone(), (Funcion)Vx.Clone());

            return m;
        }
    }
}
