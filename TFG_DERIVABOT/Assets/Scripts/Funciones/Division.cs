using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Division : Funcion
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

            Resta r = ManagerFunciones.Instance.GetFuncion<Resta>();
            r.Init(m1, m2);

            Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>();
            p.Init(1, (Funcion)Vx.Clone(), 2);

            Division d = ManagerFunciones.Instance.GetFuncion<Division>();
            d.Init(r, p);

            return d;
        }

        public override object Clone()
        {
            Division d = ManagerFunciones.Instance.GetFuncion<Division>();
            d.Init((Funcion)Ux.Clone(), (Funcion)Vx.Clone());

            return d;
        }

        public override void EscalarI(SpriteRenderer scaler)
        {
            Vx.EscalarI(_scaleVx);
            Ux.EscalarI(_scaleUx);
        }
    }
}
