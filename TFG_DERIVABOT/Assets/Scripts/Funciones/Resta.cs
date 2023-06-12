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
        private ControladorC _UxC, _VxC;

        [SerializeField]
        private Transform _Operador;

        public void Init(Funcion uX, Funcion vX)
        {
            Ux = uX;
            Vx = vX;
        }

        public override Funcion Derivada()
        {
            Funcion dUx = Ux.Derivada();
            Funcion dVx = Vx.Derivada();

            if (dUx && dVx)
                return ManagerFunciones.Instance.GetFuncion<Resta>(dUx, dVx);
            else if (dUx && !dVx)
                return dUx;
            else if (!dUx && dVx)
                return dVx;
            else 
                return null;
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
            return ManagerFunciones.Instance.GetFuncion<Resta>(Ux.Clone(), Vx.Clone());
        }

        public override void Escalar()
        {
            Ux.Escalar();
            Vx.Escalar();

            _UxC.EncajarFuncion(Ux, true);
            _VxC.EncajarFuncion(Vx, false);


            _Operador.transform.position = new Vector2(
                transform.position.x,
                _UxC.Altura() > _VxC.Altura() ?
                _VxC.GetPunto(Punto.W).position.y :
                _UxC.GetPunto(Punto.E).position.y);

            float maxY = Math.Max(
                        _UxC.GetPunto(Punto.N).position.y,
                        _VxC.GetPunto(Punto.N).position.y);

            float minY = Math.Min(
                        _UxC.GetPunto(Punto.S).position.y,
                        _VxC.GetPunto(Punto.S).position.y);

            float maxYHorizontal = Math.Max(_UxC.GetPunto(Punto.W).position.y,
                                            _VxC.GetPunto(Punto.E).position.y);

            float pMedioHorizontal = (_UxC.GetPunto(Punto.W).position.x + _VxC.GetPunto(Punto.E).position.x) / 2;

            anclajes.GetPunto(Punto.N).position = new Vector2(pMedioHorizontal, maxY);
            anclajes.GetPunto(Punto.S).position = new Vector2(pMedioHorizontal, minY);
            anclajes.GetPunto(Punto.E).position = new Vector2(_VxC.GetPunto(Punto.E).position.x, maxYHorizontal);
            anclajes.GetPunto(Punto.W).position = new Vector2(_UxC.GetPunto(Punto.W).position.x, maxYHorizontal);
        }

        public override Funcion CheckEstado()
        {
            Ux = Ux.CheckEstado();
            Vx = Vx.CheckEstado();

            if (Ux && Vx)
                return this;
            else if (Ux && !Vx)
                return Ux;
            else if (!Ux && Vx)
                return Vx;
            else return null;
        }
    }
}
