using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Division : Funcion
    {
        public Funcion Ux;
        public Funcion Vx;

        public Division(Funcion uX, Funcion vX)
            : base(Type.Division)
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

            Resta s = new Resta(m1, m2);

            Potencial p = new Potencial(1, (Funcion)Vx.Clone(), 2);

            Division d = new Division(s, p);

            return d;
        }

        public override object Clone()
        {
            Division m = new Division((Funcion)Ux.Clone(), (Funcion)Vx.Clone());

            return m;
        }
    }
}
