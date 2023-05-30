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
        public int Exponente = 100;
        public int K = 50;

        //[SerializeField]
        //private TMP_Text _textoBateria, _textoEnergia;
        //[SerializeField]
        //private GameObject _bateria;

        [SerializeField]
        private GameObject _robot;

        public void Init(int k, int exponente)
        {
            Exponente = exponente;
            K = k;
        }

        public override Funcion Derivada()
        {
            return ManagerFunciones.Instance.GetFuncion<Incognita>(K * Exponente, Exponente - 1);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Incognita>(K, Exponente);
        }

        public override void Swap(Funcion oldFx, Funcion newFx)
        {
        }
    }
}
