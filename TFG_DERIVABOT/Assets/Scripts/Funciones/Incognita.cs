using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Incognita : Funcion
    {
        public int Exponente = 100;
        public int K = 50;

        [SerializeField]
        private TMP_Text _textoBateria, _textoEnergia;
        [SerializeField]
        private GameObject _bateria;

        //void Update()
        //{

        //    // Actualizar los textos en el gameobject
        //    _textoBateria.text = Exponente.ToString();
        //    _textoEnergia.text = K.ToString();

        //    // Ocultar el gameobject si la batería llega a cero
        //    if (Exponente <= 0f)
        //    {
        //        _bateria.SetActive(false);
        //    }
        //}

        //void OnMouseDown()
        //{
        //    // Reducir la batería en 1 cuando se hace clic en el gameobject
        //    Exponente -= 1;
        //    _textoEnergia.text = K.ToString();
        //    if (Exponente <= 0f)
        //    {
        //        _bateria.SetActive(false);
        //    }
        //}

        public void Init(int k, int exponente)
        {
            Exponente = exponente;
            K = k;
        }

        public override Funcion Derivada()
        {
            Incognita i = ManagerFunciones.Instance.GetFuncion<Incognita>();
            i.Init(K * Exponente, Exponente - 1);

            return i;
        }

        public override object Clone()
        {
            Incognita i = ManagerFunciones.Instance.GetFuncion<Incognita>();
            i.Init(K, Exponente);
            return i;
        }

        public override void EscalarI(SpriteRenderer scaler)
        {
        }
    }
}
