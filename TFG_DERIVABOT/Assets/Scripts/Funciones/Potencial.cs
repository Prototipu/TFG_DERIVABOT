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
            Fx.Escalar(_scaleFx);
        }

        public override Funcion Derivada()
        {

            Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>();

            p.Init(K * Exponente, (Funcion)Fx.Clone(), Exponente - 1);

            Multiplicacion m = ManagerFunciones.Instance.GetFuncion<Multiplicacion>();
            m.Init(p, Fx.Derivada());

            return m;
        }

        public override object Clone()
        {
            Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>();

            p.Init(K, (Funcion)Fx.Clone(), Exponente);

            return p;
        }

        public override void EscalarI(SpriteRenderer scaler)
        {
            Fx.EscalarI(_scaleFx);
        }
    }
}
