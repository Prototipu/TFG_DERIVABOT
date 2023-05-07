using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Derivadas_LIB.Funciones
{
    public class Exponencial : Funcion
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
            Exponencial e = ManagerFunciones.Instance.GetFuncion<Exponencial>(Fx.Clone());

            Multiplicacion m = ManagerFunciones.Instance.GetFuncion<Multiplicacion>(Fx.Derivada(), e);

            return m;
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Exponencial>(Fx.Clone());
        }

        public override Vector2 Escalar()
        {
            Vector2 bFx = Fx.Escalar();

            _espacio.transform.localScale = new Vector2(bFx.x,
                bFx.y / _scaleFx.transform.localScale.y);

            Fx.transform.position = _scaleFx.transform.position;

            return _espacio.transform.localScale;
        }
    }
}
