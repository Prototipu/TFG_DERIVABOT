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
        
        public void Init(Funcion uX, Funcion vX)
        {
            Ux = uX;
            Vx = vX;
        }

        public override Funcion Derivada()
        {
            Suma suma = ManagerFunciones.Instance.GetFuncion<Suma>(Ftype);
            suma.Init(Ux.Derivada(), Vx.Derivada());
            return suma;
        }

        public override object Clone()
        {
            Suma suma = ManagerFunciones.Instance.GetFuncion<Suma>(Ftype);
            suma.Init((Funcion)Ux.Clone(), (Funcion)Vx.Clone());
            return suma;
        }
    }
}
