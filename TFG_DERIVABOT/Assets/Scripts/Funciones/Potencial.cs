using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB.Funciones
{
    public class Potencial : Funcion
    {
        public int Exponente;

        public Funcion Fx;

        public int K;

        public void Init(int k, Funcion fX, int exponente)
        {
            Exponente = exponente;
            Fx = fX;
            K = k;
        }

        public override Funcion Derivada()
        {

            Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>(Ftype);

            p.Init(K * Exponente, (Funcion)Fx.Clone(), Exponente - 1);

            Multiplicacion m = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Ftype);
            m.Init(p, Fx.Derivada());

            return m;
        }

        public override object Clone()
        {
            Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>(Ftype);

            p.Init(K, (Funcion)Fx.Clone(), Exponente);

            return p;
        }
    }
}
