using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

namespace Derivadas_LIB.Funciones
{
    public class Logaritmica : Funcion
    {
        public Funcion Fx;

        [SerializeField]
        private Anclajes _operador;

        [SerializeField]
        private ControladorCaja _caja;

        private float _operadorEspacioBase;

        private void Awake()
        {
            SpriteRenderer sprite = _operador.transform.parent.GetComponent<SpriteRenderer>();

            if (!sprite)
            {
                Destroy(gameObject);
                throw new Exception($"El exponencial no tiene un SpriteRenderer asignado");
            }

            _operadorEspacioBase = sprite.bounds.size.y;
        }

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
            Fx.Escalar();

            _caja.EncajarFuncion(Fx, false);


            float maxY = _caja.GetPunto(Punto.N).position.y;
            float minY = _caja.GetPunto(Punto.S).position.y;


            float ratio = ((maxY - minY) * 0.25f) / _operadorEspacioBase;

            _operador.transform.parent.localScale = Vector2.one * ratio;

            _operador.Anclar(_caja.GetPunto(Punto.W), Punto.E);

            float pMedioHorizontal = (_caja.GetPunto(Punto.E).position.x + _operador.GetPunto(Punto.W).position.x) / 2;

            anclajes.GetPunto(Punto.N).position = new Vector2(pMedioHorizontal, maxY);
            anclajes.GetPunto(Punto.S).position = new Vector2(pMedioHorizontal, minY);
            anclajes.GetPunto(Punto.E).position = _caja.GetPunto(Punto.E).position;
            anclajes.GetPunto(Punto.W).position = _operador.GetPunto(Punto.W).position;

        }

        public void Cargar()
        {
            ManagerFunciones.Instance.CargarLogaritmica(this);
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
