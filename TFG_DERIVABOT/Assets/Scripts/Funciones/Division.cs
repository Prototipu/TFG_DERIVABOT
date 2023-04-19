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

            Resta r = ManagerFunciones.Instance.GetFuncion<Resta>(Ftype);
            r.Init(m1, m2);

            Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>(Ftype);
            p.Init(1, (Funcion)Vx.Clone(), 2);

            Division d = ManagerFunciones.Instance.GetFuncion<Division>(Ftype);
            d.Init(r, p);

            return d;
        }

        public override object Clone()
        {
            Division d = ManagerFunciones.Instance.GetFuncion<Division>(Ftype);
            d.Init((Funcion)Ux.Clone(), (Funcion)Vx.Clone());

            return d;
        }
    }
}
