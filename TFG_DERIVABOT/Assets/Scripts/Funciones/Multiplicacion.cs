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
        }

        public override Funcion Derivada()
        {
            Funcion dUx = Ux.Derivada();
            Funcion dVx = Vx.Derivada();

            Multiplicacion m1 = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(dUx, Vx.Clone());

            Multiplicacion m2 = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Ux.Clone(), dVx);

            return ManagerFunciones.Instance.GetFuncion<Suma>(m1, m2);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Ux.Clone(), Vx.Clone());
        }

        public override Vector2 Escalar()
        {
            Vector2 bUx = Ux.Escalar();
            Vector2 bVx = Vx.Escalar();

            _espacio.transform.localScale = new Vector2(Math.Max(bUx.x, bVx.x) / _scaleUx.transform.localScale.x, Math.Max(bUx.y, bVx.y));

            Ux.transform.position = _scaleUx.transform.position;

            Vx.transform.position = _scaleVx.transform.position;

            return _espacio.transform.localScale;
        }
    }
}
