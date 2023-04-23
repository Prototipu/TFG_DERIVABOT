using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Logaritmica : Funcion
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
            Division d = ManagerFunciones.Instance.GetFuncion<Division>();
            d.Init(Fx.Derivada(), (Funcion)Fx.Clone());

            return d;
        }

        public override object Clone()
        {
            Logaritmica l = ManagerFunciones.Instance.GetFuncion<Logaritmica>();
            l.Init((Funcion)Fx.Clone());
            return l;
        }

        public override void EscalarI(SpriteRenderer scaler)
        {
            Fx.EscalarI(_scaleFx);
        }
    }
}
