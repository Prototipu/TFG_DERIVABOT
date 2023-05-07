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
            return ManagerFunciones.Instance.GetFuncion<Division>(Fx.Derivada(), Fx.Clone());
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Logaritmica>(Fx.Clone());
        }

        public override Vector2 Escalar()
        {
            Vector2 bFx = Fx.Escalar();

            _espacio.transform.localScale = new Vector2(bFx.x + _extra.transform.localScale.x, bFx.y);

            return _espacio.transform.localScale;
        }
    }
}
