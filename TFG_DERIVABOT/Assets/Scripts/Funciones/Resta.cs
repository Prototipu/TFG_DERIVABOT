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

        public void Init(Funcion uX, Funcion vX)
        {
            Ux = uX;
            Vx = vX;
        }

        public override Funcion Derivada()
        {
            Resta resta = ManagerFunciones.Instance.GetFuncion<Resta>(Ftype);
            resta.Init(Ux.Derivada(), Vx.Derivada());
            return resta;
        }

        public override object Clone()
        {
            Resta resta = ManagerFunciones.Instance.GetFuncion<Resta>(Ftype);
            resta.Init((Funcion)Ux.Clone(), (Funcion)Vx.Clone());
            return resta;
        }
    }
}
