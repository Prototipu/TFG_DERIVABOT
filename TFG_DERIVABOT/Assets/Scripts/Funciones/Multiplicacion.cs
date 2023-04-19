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

        public void Init(Funcion uX, Funcion vX)
        {
            Ux = uX;
            Vx = vX;
        }

        public override Funcion Derivada()
        {
            Funcion dUx = Ux.Derivada();
            Funcion dVx = Vx.Derivada();

            Multiplicacion m1 = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Ftype);
            m1.Init(dUx, (Funcion)Vx.Clone());

            Multiplicacion m2 = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Ftype);
            m1.Init((Funcion)Ux.Clone(), dVx);

            Suma s = ManagerFunciones.Instance.GetFuncion<Suma>(Ftype);
            s.Init(m1, m2);

            return s;
        }

        public override object Clone()
        {
            Multiplicacion m = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Ftype);
            m.Init((Funcion)Ux.Clone(), (Funcion)Vx.Clone());

            return m;
        }
    }
}
