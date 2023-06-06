using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Potencial : Funcion
    {
        public int Exponente;

        public Funcion Fx;

        public int K;

        [SerializeField]
        private SpriteRenderer _scaleFx;

        public void Init(int k, Funcion fX, int exponente)
        {
            Exponente = exponente;
            Fx = fX;
            K = k;
        }

        public override Funcion Derivada()
        {
            Funcion dFx = Fx.Derivada();

            if (dFx)
            {
                Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>(K * Exponente, Fx.Clone(), Exponente - 1);

                return ManagerFunciones.Instance.GetFuncion<Multiplicacion>(p,dFx);
            }
            else
                return null;
        }

        public override void Swap(Funcion oldFx, Funcion newFx)
        {
            Init(K, newFx, Exponente);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Potencial>(K, (Funcion)Fx.Clone(), Exponente);
        }

        public override void Escalar()
        {
            Fx.Escalar();
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
