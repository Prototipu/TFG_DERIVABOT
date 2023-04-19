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

        public void Init(Funcion fX)
        {
            Fx = fX;
        }

        public override Funcion Derivada()
        {
            Exponencial e = ManagerFunciones.Instance.GetFuncion<Exponencial>(Ftype);
            e.Init((Funcion)Fx.Clone());

            Multiplicacion m = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Ftype);
            m.Init(Fx.Derivada(), e);

            return m;
        }

        public override object Clone()
        {
            Exponencial e = ManagerFunciones.Instance.GetFuncion<Exponencial>(Ftype);
            e.Init((Funcion)Fx.Clone());

            return e;
        }
    }
}
