using System;
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
        public Funcion Ux, Vx;

        [SerializeField]
        private Anclajes _operador;

        public void Init(Funcion uX, Funcion vX)
        {
            Ux = uX;
            Vx = vX;

            Anclajes UxA = Ux.anclajes;
            Anclajes VxA = Vx.anclajes;

            UxA.Anclar(_operador.GetPunto(Punto.N), Punto.S);
            VxA.Anclar(_operador.GetPunto(Punto.S), Punto.N);

            float maxX = Math.Max(
                        UxA.GetPunto(Punto.E).position.x,
                        VxA.GetPunto(Punto.E).position.x);

            float minX = Math.Min(
                        UxA.GetPunto(Punto.W).position.x,
                        VxA.GetPunto(Punto.W).position.x);


            float pMedioVertical = (UxA.GetPunto(Punto.N).position.y + VxA.GetPunto(Punto.S).position.y) / 2;

            anclajes.GetPunto(Punto.N).position = new Vector2(transform.position.x, UxA.GetPunto(Punto.N).position.y);
            anclajes.GetPunto(Punto.S).position = new Vector2(transform.position.x, VxA.GetPunto(Punto.S).position.y);
            anclajes.GetPunto(Punto.E).position = new Vector2(maxX, pMedioVertical);
            anclajes.GetPunto(Punto.W).position = new Vector2(minX, pMedioVertical);
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

        public override void Swap(Funcion oldFx, Funcion newFx)
        {
            if (oldFx == Ux)
                Init(newFx, Vx);
            else
                Init(Ux, newFx);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Division>(Ux.Clone(), Vx.Clone());
        }
    }
}
