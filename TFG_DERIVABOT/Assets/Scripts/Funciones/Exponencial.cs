using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace Derivadas_LIB.Funciones
{
    public class Exponencial : Funcion
    {
        public Funcion Fx;

        [SerializeField]
        private Anclajes _operador, _barra;

        private float _barraEspacioBase;

        private void Awake()
        {
            SpriteRenderer barra = _barra.transform.parent.GetComponent<SpriteRenderer>();

            if (!barra)
            {
                Destroy(gameObject);
                throw new Exception($"El exponencial no tiene un SpriteRenderer asignado");
            }

            _barraEspacioBase = barra.bounds.size.x;
        }

        public void Init(Funcion fX)
        {
            Fx = fX;
        }

        public override Funcion Derivada()
        {

            Funcion dFx = Fx.Derivada();

            if (dFx)
            {
                Exponencial e = ManagerFunciones.Instance.GetFuncion<Exponencial>(Fx.Clone());

                return ManagerFunciones.Instance.GetFuncion<Multiplicacion>(e, dFx);
            }
            else
                return null;
        }

        public override void Swap(Funcion oldFx, Funcion newFx)
        {
            Init(newFx);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Exponencial>(Fx.Clone());
        }

        public override void Escalar()
        {
            Fx.Escalar();

            Anclajes FxA = Fx.anclajes;


            _barra.Anclar(_operador.GetPunto(Punto.N), Punto.S);

            FxA.Anclar(_barra.GetPunto(Punto.N), Punto.S);

            float maxX = Math.Max(
                        FxA.GetPunto(Punto.E).position.x,
                        _operador.GetPunto(Punto.E).position.x);

            float minX = Math.Min(
                        FxA.GetPunto(Punto.W).position.x,
                        _operador.GetPunto(Punto.W).position.x);

            float pMedioVertical = (FxA.GetPunto(Punto.N).position.y + _operador.GetPunto(Punto.S).position.y) / 2;

            anclajes.GetPunto(Punto.N).position = new Vector2(transform.position.x, FxA.GetPunto(Punto.N).position.y);
            anclajes.GetPunto(Punto.S).position = new Vector2(transform.position.x, _operador.GetPunto(Punto.S).position.y);
            anclajes.GetPunto(Punto.E).position = new Vector2(maxX, pMedioVertical);
            anclajes.GetPunto(Punto.W).position = new Vector2(minX, pMedioVertical);


            float ratio = (maxX - minX) / _barraEspacioBase;

            _barra.transform.parent.localScale = new Vector3(ratio, 0.1f, 1);
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
