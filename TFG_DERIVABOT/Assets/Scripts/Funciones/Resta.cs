using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Resta : Funcion
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
            Resta resta = ManagerFunciones.Instance.GetFuncion<Resta>();
            resta.Init(Ux.Derivada(), Vx.Derivada());
            return resta;
        }

        public override object Clone()
        {
            Resta resta = ManagerFunciones.Instance.GetFuncion<Resta>();
            resta.Init((Funcion)Ux.Clone(), (Funcion)Vx.Clone());
            return resta;
        }

        public override void EscalarI(SpriteRenderer scaler)
        {
            Vx.EscalarI(_scaleVx);
            Ux.EscalarI(_scaleUx);
        }
    }
}
