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
        private Anclajes _operador;

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

                return ManagerFunciones.Instance.GetFuncion<Multiplicacion>(dFx, e);
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

            FxA.Anclar(_operador.GetPunto(Punto.N), Punto.S);

            Vector2 max = new Vector2(
                FxA.GetPunto(Punto.E).position.x,
                FxA.GetPunto(Punto.N).position.y);

            Vector2 min = new Vector2(
               FxA.GetPunto(Punto.W).position.x,
               anclajes.GetPunto(Punto.S).position.y);

            anclajes.GetPunto(Punto.N).position = new Vector2(transform.position.x, max.y);
            anclajes.GetPunto(Punto.S).position = new Vector2(transform.position.x, min.y);
            anclajes.GetPunto(Punto.E).position = new Vector2(max.x, transform.position.y);
            anclajes.GetPunto(Punto.W).position = new Vector2(min.x, transform.position.y);
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
