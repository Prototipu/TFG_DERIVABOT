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

            UxA.Anclar(_operador.GetPunto(Punto.N), Punto.S);
            VxA.Anclar(_operador.GetPunto(Punto.S), Punto.N);

            Vector2 max = new Vector2(
                Math.Max(UxA.GetPunto(Punto.E).position.x,
                        VxA.GetPunto(Punto.E).position.x),
                UxA.GetPunto(Punto.N).position.y);

            Vector2 min = new Vector2(
                Math.Min(UxA.GetPunto(Punto.W).position.x,
                        VxA.GetPunto(Punto.W).position.x),
                VxA.GetPunto(Punto.S).position.y);

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

            Resta r = ManagerFunciones.Instance.GetFuncion<Resta>(m1, m2);

            Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>(1, (Funcion)Vx.Clone(), 2);

            return ManagerFunciones.Instance.GetFuncion<Division>(r, p);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Division>(Ux.Clone(), Vx.Clone());
        }
    }
}
