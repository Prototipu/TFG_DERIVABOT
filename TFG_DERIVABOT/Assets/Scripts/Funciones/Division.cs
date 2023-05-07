﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

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
        }

        public override Funcion Derivada()
        {
            Funcion dUx = Ux.Derivada();
            Funcion dVx = Vx.Derivada();

            Multiplicacion m1 = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(dUx, Vx.Clone());

            Multiplicacion m2 = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Ux.Clone(), dVx);

            Resta r = ManagerFunciones.Instance.GetFuncion<Resta>(m1, m2);

            Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>(1, (Funcion)Vx.Clone(), 2);

            return ManagerFunciones.Instance.GetFuncion<Division>(r, p);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Division>(Ux.Clone(), Vx.Clone());
        }

        public override Vector2 Escalar()
        {
            Vector2 bUx = Ux.Escalar();
            Vector2 bVx = Vx.Escalar();

            _espacio.transform.localScale = new Vector2(Math.Max(bUx.x, bVx.x),
                Math.Max(bUx.y, bVx.y) / _scaleUx.transform.localScale.y);

            Ux.transform.position = _scaleUx.transform.position;

            Vx.transform.position = _scaleVx.transform.position;

            return _espacio.transform.localScale;
        }
    }
}
