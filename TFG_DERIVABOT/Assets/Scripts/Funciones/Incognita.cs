using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Incognita : Funcion
    {
        public int Exponente;
        public int K;
        public bool Derivado;
        public bool Reciclable = false;
        private bool Reciclado = false;


        [SerializeField]
        private TMP_Text _textoBateria, _textoEnergia;

        [SerializeField]
        private GameObject _robot;

        public void Init(int k, int exponente, bool derivado)
        {
            Exponente = exponente;
            K = k;
            Derivado = derivado;

            if (!Derivado && exponente == 0)
            {
                Reciclable = true;
                ; // TODO CAMBIAR SPRITE A RECICLABLE
            }
        }

        public override Funcion Derivada()
        {
            if (Exponente == 0)
                return null;
            return ManagerFunciones.Instance.GetFuncion<Incognita>(K * Exponente, Exponente - 1, true);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Incognita>(K, Exponente, Derivado);
        }

        public override void Swap(Funcion oldFx, Funcion newFx)
        { }

        public override void Escalar()
        { }

        public override Funcion CheckEstado()
        {
            if (Reciclado)
                return null;
            return this;
        }

        public void Reciclar()
        {
            if (Reciclable)
            {
                Reciclado = true;
                ManagerFunciones.Instance.Reciclar(this);
            }
        }

        public void Cargar()
        {

        }
    }
}
