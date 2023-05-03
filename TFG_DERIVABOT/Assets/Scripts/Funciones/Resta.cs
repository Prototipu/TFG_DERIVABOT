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
        private SpriteRenderer _scaleUx, _scaleVx, _operador;

        public void Init(Funcion uX, Funcion vX)
        {
            Ux = uX;
            Vx = vX;
        }

        public override Funcion Derivada()
        {
            return ManagerFunciones.Instance.GetFuncion<Resta>(Ux.Derivada(), Vx.Derivada());
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Resta>(Ux.Clone(), Vx.Clone());

        }

        public override Bounds Escalar()
        {
            transform.localScale = Vector3.one;

            Bounds bUx = Ux.Escalar();
            Bounds bVx = Vx.Escalar();

            Bounds bEspacio = _espacio.bounds;

            float maxH = Math.Max(bUx.size.y, bVx.size.y);

            float ratio = maxH / bEspacio.size.y;

            transform.localScale = new Vector3(1, ratio, 1);

            _scaleUx.transform.localScale = Vector3.one;
            _scaleVx.transform.localScale = Vector3.one;

            Bounds bScaleUx = _scaleUx.bounds;
            Bounds bScaleVx = _scaleVx.bounds;

            float ratioUx = bUx.size.x / bScaleUx.size.x;
            float ratioVx = bVx.size.x / bScaleVx.size.x;

            _scaleUx.transform.localScale = new Vector3(ratioUx, 1, 1);
            _scaleUx.transform.localPosition = new Vector3(-_operador.bounds.extents.x - _scaleUx.transform.localScale.x / 2f, 0, 0);
            _scaleVx.transform.localScale = new Vector3(ratioVx, 1, 1);
            _scaleVx.transform.localPosition = new Vector3(_operador.bounds.extents.x + _scaleVx.transform.localScale.x / 2f, 0, 0);

            Ux.transform.position = _scaleUx.transform.position + Vector3.up * ((bUx.extents.y) - Mathf.Abs(bScaleUx.min.y - _scaleUx.transform.position.y));

            Vx.transform.position = _scaleVx.transform.position + Vector3.up * ((bVx.extents.y) - Mathf.Abs(bScaleVx.min.y - _scaleVx.transform.position.y));

            Vector3 targetSize = new Vector3(bScaleUx.size.x + bScaleVx.size.x + _operador.bounds.size.x, bEspacio.size.y, 0);

            Bounds newBounds = _espacio.bounds;
            newBounds.size = targetSize;

            _espacio.bounds = newBounds;

            return newBounds;
        }
    }
}
