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

        public void Init(Funcion fX)
        {
            Fx = fX;
        }

        public override Funcion Derivada()
        {
            Division d = ManagerFunciones.Instance.GetFuncion<Division>(Ftype);
            d.Init(Fx.Derivada(), (Funcion)Fx.Clone());

            return d;
        }

        public override object Clone()
        {
            Logaritmica l = ManagerFunciones.Instance.GetFuncion<Logaritmica>(Ftype);
            l.Init((Funcion)Fx.Clone());
            return l;
        }
    }
}
