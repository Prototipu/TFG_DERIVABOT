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
        }

        public override Funcion Derivada()
        {
            return ManagerFunciones.Instance.GetFuncion<Suma>(Ux.Derivada(), Vx.Derivada());
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Suma>(Ux.Clone(), Vx.Clone());
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
