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

        void Update()
        {
            Bounds combinedBounds = ParserFunciones.BoundsCombinados(_robot);

            Vector3 min = combinedBounds.min;
            Vector3 max = combinedBounds.max;

            Debug.DrawLine(new Vector3(min.x, min.y, 0), new Vector3(max.x, min.y, 0), Color.red, 5f);
            Debug.DrawLine(new Vector3(max.x, min.y, 0), new Vector3(max.x, max.y, 0), Color.red, 5f);
            Debug.DrawLine(new Vector3(max.x, max.y, 0), new Vector3(min.x, max.y, 0), Color.red, 5f);
            Debug.DrawLine(new Vector3(min.x, max.y, 0), new Vector3(min.x, min.y, 0), Color.red, 5f);
        }

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

        public override Vector2 Escalar()
        {
            return _espacio.transform.localScale;
        }
    }
}
