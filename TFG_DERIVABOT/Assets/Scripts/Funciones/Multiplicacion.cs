using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Multiplicacion : Funcion
    {
        public Funcion Ux;
        public Funcion Vx;

        [SerializeField]
        private SpriteRenderer _scaleUx, _scaleVx;

        public void Init(Funcion uX, Funcion vX)
        {
            Ux = uX;
            Vx = vX;

            Ux.Escalar(_scaleUx);
            Vx.Escalar(_scaleVx);
        }

        public override Funcion Derivada()
        {
            Funcion dUx = Ux.Derivada();
            Funcion dVx = Vx.Derivada();

            Multiplicacion m1 = ManagerFunciones.Instance.GetFuncion<Multiplicacion>();
            m1.Init(dUx, (Funcion)Vx.Clone());

            Multiplicacion m2 = ManagerFunciones.Instance.GetFuncion<Multiplicacion>();
            m2.Init((Funcion)Ux.Clone(), dVx);

            Suma s = ManagerFunciones.Instance.GetFuncion<Suma>();
            s.Init(m1, m2);

            return s;
        }

        public override object Clone()
        {
            Multiplicacion m = ManagerFunciones.Instance.GetFuncion<Multiplicacion>();
            m.Init((Funcion)Ux.Clone(), (Funcion)Vx.Clone());

            return m;
        }

        public override void EscalarI(SpriteRenderer scaler)
        {
            Vx.EscalarI(_scaleVx);
            Ux.EscalarI(_scaleUx);
        }
    }
}
