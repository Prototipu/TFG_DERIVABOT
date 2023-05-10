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
        private Anclajes _operador;

        public void Init(Funcion uX, Funcion vX)
        {
            Ux = uX;
            Vx = vX;

            Anclajes UxA = Ux.anclajes;
            Anclajes VxA = Vx.anclajes;

            UxA.Anclar(_operador.GetPunto(Punto.W), Punto.E);
            VxA.Anclar(_operador.GetPunto(Punto.E), Punto.W);

            Vector2 max = new Vector2(
                VxA.GetPunto(Punto.E).position.x,
                Math.Max(UxA.GetPunto(Punto.N).position.y,
                        VxA.GetPunto(Punto.N).position.y));

            Vector2 min = new Vector2(
                UxA.GetPunto(Punto.W).position.x,
                Math.Min(UxA.GetPunto(Punto.S).position.y,
                        VxA.GetPunto(Punto.S).position.y));

            anclajes.GetPunto(Punto.N).position = new Vector2(transform.position.x, max.y);
            anclajes.GetPunto(Punto.S).position = new Vector2(transform.position.x, min.y);
            anclajes.GetPunto(Punto.E).position = new Vector2(max.x, transform.position.y);
            anclajes.GetPunto(Punto.W).position = new Vector2(min.x, transform.position.y);
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
    }
}
