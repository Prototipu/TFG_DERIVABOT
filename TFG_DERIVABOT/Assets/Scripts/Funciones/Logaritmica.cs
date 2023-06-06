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
        }

        public override Funcion Derivada()
        {
            Funcion dFx = Fx.Derivada();

            if (dFx)
                return ManagerFunciones.Instance.GetFuncion<Division>(dFx, Fx.Clone());
            else
                return null;
        }

        public override void Swap(Funcion oldFx, Funcion newFx)
        {
            Init(newFx);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Logaritmica>(Fx.Clone());
        }

        public override void Escalar()
        {
            throw new NotImplementedException();
        }

        public override Funcion CheckEstado()
        {
            Fx = Fx.CheckEstado();

            if (Fx)
                return this;
            else return null;
        }
    }
}
