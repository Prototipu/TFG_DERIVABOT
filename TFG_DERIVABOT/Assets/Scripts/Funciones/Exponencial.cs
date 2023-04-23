using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Exponencial : Funcion
    {
        public Funcion Fx;

        [SerializeField]
        private SpriteRenderer _scaleFx;

        public void Init(Funcion fX)
        {
            Fx = fX;
            Fx.Escalar(_scaleFx);
        }

        public override Funcion Derivada()
        {
            Exponencial e = ManagerFunciones.Instance.GetFuncion<Exponencial>();
            e.Init((Funcion)Fx.Clone());

            Multiplicacion m = ManagerFunciones.Instance.GetFuncion<Multiplicacion>();
            m.Init(Fx.Derivada(), e);

            return m;
        }

        public override object Clone()
        {
            Exponencial e = ManagerFunciones.Instance.GetFuncion<Exponencial>();
            e.Init((Funcion)Fx.Clone());

            return e;
        }

        public override void EscalarI(SpriteRenderer scaler)
        {
            Fx.EscalarI(_scaleFx);
        }
    }
}
