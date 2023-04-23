using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Suma : Funcion
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
            Suma suma = ManagerFunciones.Instance.GetFuncion<Suma>();
            suma.Init(Ux.Derivada(), Vx.Derivada());
            return suma;
        }

        public override object Clone()
        {
            Suma suma = ManagerFunciones.Instance.GetFuncion<Suma>();
            suma.Init((Funcion)Ux.Clone(), (Funcion)Vx.Clone());
            return suma;
        }

        public override void EscalarI(SpriteRenderer scaler)
        {
            Vx.EscalarI(_scaleVx);
            Ux.EscalarI(_scaleUx);
        }
    }
}
